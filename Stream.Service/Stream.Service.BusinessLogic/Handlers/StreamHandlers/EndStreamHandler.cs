using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class EndStreamHandler: IRequestHandler<EndStreamCommand>
{
    private readonly IStreamRepository _repository;

    public EndStreamHandler(IStreamRepository repository)
    {
        _repository = repository;
    }


    public async Task Handle(EndStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = await _repository.GetByIdAsync(request.StreamId, cancellationToken)
                             ?? throw new NotFoundException("Stream with this id does not exist");
        
        if (stream.EndTime != null)
        {
            throw new BadRequestException("You cannot end stream that already ended");
        }

        await _repository.EndStreamAsync(request.StreamId, cancellationToken);
    }
}