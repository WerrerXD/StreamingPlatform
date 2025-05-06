using MediatR;

namespace Stream.Service.BusinessLogic.Contracts;

public record CreateChatMessageDto(
    string UserId,
    string Message
    );