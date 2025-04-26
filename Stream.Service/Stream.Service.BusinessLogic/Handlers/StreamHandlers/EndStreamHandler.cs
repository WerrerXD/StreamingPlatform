using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class EndStreamHandler: IRequestHandler<EndStreamCommand>
{
    private readonly IStreamRepository _repository;
    private readonly ILoggingService _loggingService;

    public EndStreamHandler(IStreamRepository repository, ILoggingService loggingService)
    {
        _repository = repository;
        _loggingService = loggingService;
    }


    public async Task Handle(EndStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = await _repository.GetByIdAsync(request.StreamId, cancellationToken)
            ?? throw new NotFoundException("Stream with this id does not exist");
        
        if (stream.EndTime != null)
        {
            throw new BadRequestException("You cannot end stream that already ended");
        }

        stream.EndTime = DateTime.UtcNow;
        
        await _repository.UpdateAsync(stream, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Stream with id: {stream.Id} was successfully ended");
    }
}