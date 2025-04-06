using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers;

public class GetStreamByIdHandler: IRequestHandler<GetStreamByIdQuery, StreamModel>
{
    private readonly IStreamRepository _repository;

    public GetStreamByIdHandler(IStreamRepository repository)
    {
        _repository = repository;
    }

    public async Task<StreamModel> Handle(GetStreamByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.StreamId);
    }
}