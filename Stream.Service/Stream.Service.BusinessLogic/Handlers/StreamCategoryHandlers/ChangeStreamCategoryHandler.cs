using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamCategoryHandlers;

public class ChangeStreamCategoryHandler : IRequestHandler<ChangeStreamCategoryCommand>
{
    private readonly IStreamCategoryRepository _repository;
    private readonly IMapper _mapper;

    public ChangeStreamCategoryHandler(IStreamCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task Handle(ChangeStreamCategoryCommand request, CancellationToken cancellationToken)
    {
        var _ = await _repository.GetStreamCategoryById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Stream category does not exist");
        
        if (!string.IsNullOrEmpty(request.Name))
        {
            var testStreamCategory =
                await _repository.GetStreamCategoryByName(request.Name, cancellationToken);
            if (testStreamCategory != null)
            {
                throw new AlreadyExistsException("Stream Category with this name already exists");
            }
            await _repository.SetStreamCategoryName(request.Id, request.Name, cancellationToken);
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            await _repository.SetStreamCategoryDescription(request.Id, request.Description, cancellationToken);
        }
    }
}