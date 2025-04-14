using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record ChangeStreamCommand(
    string StreamId,
    string? StreamName,
    string? StreamDescription,
    string? CategoryId
    )
    : IRequest;