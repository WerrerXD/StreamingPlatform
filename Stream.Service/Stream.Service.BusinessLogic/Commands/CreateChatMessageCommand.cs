using MediatR;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Commands;

public record CreateChatMessageCommand(
    string StreamId,
    CreateChatMessageDto Dto
    ) : IRequest;