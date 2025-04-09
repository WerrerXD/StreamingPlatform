using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers;

public class EndStreamHandler: IRequestHandler<EndStreamCommand>
{
    private readonly IStreamRepository _repository;

    public EndStreamHandler(IStreamRepository repository)
    {
        _repository = repository;
    }


    public async Task Handle(EndStreamCommand request, CancellationToken cancellationToken)
    {
        var streamExists = await _repository.GetByIdAsync(request.StreamId, cancellationToken)
                             ?? throw new Exception("Stream with this id does not exist");

        await _repository.EndStreamAsync(request.StreamId, cancellationToken);
    }
}