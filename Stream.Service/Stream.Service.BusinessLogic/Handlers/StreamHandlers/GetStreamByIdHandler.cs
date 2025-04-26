using MediatR;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class GetStreamByIdHandler: IRequestHandler<GetStreamByIdQuery, StreamModel>
{
    private readonly IStreamRepository _repository;

    public GetStreamByIdHandler(IStreamRepository repository)
    {
        _repository = repository;
    }

    public async Task<StreamModel> Handle(GetStreamByIdQuery request, CancellationToken cancellationToken)
    {
        var stream = await _repository.GetByIdAsync(request.StreamId, cancellationToken)
            ?? throw new NotFoundException("Stream does not exist");
        
        return stream;
    }
}