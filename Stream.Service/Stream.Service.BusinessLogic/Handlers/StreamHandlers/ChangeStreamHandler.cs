using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class ChangeStreamHandler: IRequestHandler<ChangeStreamCommand>
{
    private readonly IStreamRepository _repository;
    private readonly IStreamCategoryRepository _categoryRepository;

    public ChangeStreamHandler(IStreamRepository repository, IStreamCategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }


    public async Task Handle(ChangeStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = await _repository.GetByIdAsync(request.StreamId, cancellationToken)
                ?? throw new NotFoundException("Stream does not exist");

        if (stream.EndTime != null)
        {
            throw new BadRequestException("You cannot change stream that already ended");
        }
        
        if (!string.IsNullOrEmpty(request.StreamName))
        {
            await _repository.UpdateTitleAsync(request.StreamName, request.StreamId, cancellationToken);
        }

        if (!string.IsNullOrEmpty(request.StreamDescription))
        {
            await _repository.UpdateDescriptionAsync(request.StreamDescription, request.StreamId, cancellationToken);
        }
        
        if (!string.IsNullOrEmpty(request.CategoryId))
        {
            var categoryExists = await _categoryRepository.GetStreamCategoryById(request.CategoryId, cancellationToken) 
                                 ?? throw new NotFoundException("Category with this id does not exist");
            await _repository.UpdateCategoryAsync(request.CategoryId, request.StreamId, cancellationToken);
        }
    }
}