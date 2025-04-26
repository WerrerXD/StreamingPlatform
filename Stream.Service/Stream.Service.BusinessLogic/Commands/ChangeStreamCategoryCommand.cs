using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record ChangeStreamCategoryCommand(
    string Id,
    string? Name,
    string? Description
    ) : IRequest;