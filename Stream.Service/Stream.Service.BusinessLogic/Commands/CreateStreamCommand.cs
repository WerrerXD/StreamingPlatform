using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record CreateStreamCommand(
    string StreamerId,
    string Title,
    string Description,
    string CategoryId
) : IRequest<string>;