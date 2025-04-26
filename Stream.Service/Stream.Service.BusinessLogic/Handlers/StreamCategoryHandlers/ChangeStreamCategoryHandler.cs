using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamCategoryHandlers;

public class ChangeStreamCategoryHandler : IRequestHandler<ChangeStreamCategoryCommand>
{
    private readonly IStreamCategoryRepository _repository;
    private readonly ILoggingService _loggingService;

    public ChangeStreamCategoryHandler(IStreamCategoryRepository repository, ILoggingService loggingService)
    {
        _repository = repository;
        _loggingService = loggingService;
    }


    public async Task Handle(ChangeStreamCategoryCommand request, CancellationToken cancellationToken)
    {
        var streamCategory = await _repository.GetStreamCategoryByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundException("Stream category does not exist");
        
        if (!string.IsNullOrEmpty(request.Name))
        {
            var testStreamCategory = await _repository.GetStreamCategoryByNameAsync(request.Name, cancellationToken);
            
            if (testStreamCategory != null)
            {
                throw new AlreadyExistsException("Stream Category with this name already exists");
            }
            
            streamCategory.Name = request.Name;
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            streamCategory.Description = request.Description;
        }
        
        await _repository.UpdateAsync(streamCategory, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Stream-category with id: {streamCategory.Id} was successfully updated");
    }
}