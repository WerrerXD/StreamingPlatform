using MediatR;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class GetAllActiveStreamsHandler: IRequestHandler<GetAllActiveStreamsQuery, List<StreamModel>>
{
    private readonly IStreamRepository _repository;

    public GetAllActiveStreamsHandler(IStreamRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<List<StreamModel>> Handle(GetAllActiveStreamsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllActiveAsync(cancellationToken);
    }
}