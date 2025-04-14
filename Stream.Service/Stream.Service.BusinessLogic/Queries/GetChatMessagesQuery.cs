using MediatR;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Queries;

public record GetChatMessagesQuery(
    string StreamId
    ) : IRequest<List<ChatMessage>>;