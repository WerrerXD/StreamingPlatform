using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class ChangeStreamHandler: IRequestHandler<ChangeStreamCommand>
{
    private readonly IStreamRepository _repository;
    private readonly IStreamCategoryRepository _categoryRepository;
    private readonly ILoggingService _loggingService;

    public ChangeStreamHandler(IStreamRepository repository, IStreamCategoryRepository categoryRepository, ILoggingService loggingService)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        _loggingService = loggingService;
    }
    
    public async Task Handle(ChangeStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = await _repository.GetByIdAsync(request.StreamId, cancellationToken)
                ?? throw new NotFoundException("Stream does not exist");

        if (stream.EndTime != null)
        {
            throw new BadRequestException("You cannot change stream that already ended");
        }
        
        if (!string.IsNullOrWhiteSpace(request.Dto.StreamName))
        {
            stream.Title = request.Dto.StreamName;
        }

        if (!string.IsNullOrWhiteSpace(request.Dto.StreamDescription))
        {
            stream.Description = request.Dto.StreamDescription;
        }
        
        if (!string.IsNullOrWhiteSpace(request.Dto.CategoryId))
        {
            var category = await _categoryRepository.GetStreamCategoryByIdAsync(request.Dto.CategoryId, cancellationToken) 
                ?? throw new NotFoundException("Category with this id does not exist");
            
            stream.CategoryId = request.Dto.CategoryId;
        }
        
        await _repository.UpdateAsync(stream, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Stream with id: {stream.Id} was successfully updated");
    }
}