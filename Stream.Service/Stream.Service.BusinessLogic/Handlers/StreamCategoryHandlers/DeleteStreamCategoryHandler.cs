using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamCategoryHandlers;

public class DeleteStreamCategoryHandler: IRequestHandler<DeleteStreamCategoryCommand>
{
    private readonly IStreamCategoryRepository _repository;
    private readonly ILoggingService _loggingService;

    public DeleteStreamCategoryHandler(IStreamCategoryRepository repository, ILoggingService loggingService)
    {
        _repository = repository;
        _loggingService = loggingService;
    }
    
    public async Task Handle(DeleteStreamCategoryCommand request, CancellationToken cancellationToken)
    {
        var streamCategory = await _repository.GetStreamCategoryByIdAsync(request.CategoryId, cancellationToken) 
            ?? throw new NotFoundException("Stream category does not exist");
        
        await _repository.DeleteAsync(streamCategory, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Stream-category with id: {streamCategory.Id} was successfully deleted");
    }
}