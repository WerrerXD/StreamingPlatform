using MediatR;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Commands;

public record ChangeStreamCommand(
    string StreamId,
    ChangeStreamDto Dto
    ) : IRequest;