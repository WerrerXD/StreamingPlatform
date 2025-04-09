using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers;

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
        var streamCategory = _mapper.Map<StreamCategory>(request);
        
        var _ = await _repository.GetStreamCategoryById(streamCategory.Id, cancellationToken)
            ?? throw new Exception("Stream category does not exist");
        
        if (!string.IsNullOrEmpty(streamCategory.Name))
        {
            var testStreamCategory =
                await _repository.GetStreamCategoryByName(streamCategory.Name, cancellationToken);
            if (testStreamCategory != null)
            {
                throw new Exception("Stream Category with this name already exists");
            }
            await _repository.SetStreamCategoryName(streamCategory.Id, streamCategory.Name, cancellationToken);
        }

        if (!string.IsNullOrEmpty(streamCategory.Description))
        {
            await _repository.SetStreamCategoryDescription(streamCategory.Id, streamCategory.Description, cancellationToken);
        }
    }
}