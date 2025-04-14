using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record CreateChatMessageCommand(
    string StreamId,
    string UserId,
    string Message
) : IRequest;