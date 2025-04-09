using MediatR;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers;

public class GetAllStreamerStreamsHandler: IRequestHandler<GetAllStreamerStreamsQuery, List<StreamModel>>
{
    private readonly IStreamRepository _repository;

    public GetAllStreamerStreamsHandler(IStreamRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StreamModel>> Handle(GetAllStreamerStreamsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByStreamerAsync(request.StreamerId, cancellationToken);
    }
}