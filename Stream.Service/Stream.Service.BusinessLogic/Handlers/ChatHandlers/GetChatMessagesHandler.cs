using MediatR;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.ChatHandlers;

public class GetChatMessagesHandler : IRequestHandler<GetChatMessagesQuery, List<ChatMessage>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IStreamRepository _streamRepository;

    public GetChatMessagesHandler(IChatRepository chatRepository, IStreamRepository streamRepository)
    {
        _chatRepository = chatRepository;
        _streamRepository = streamRepository;
    }

    public async Task<List<ChatMessage>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
    {
        var stream = await _streamRepository.GetByIdAsync(request.StreamId, cancellationToken)
                     ?? throw new NotFoundException("Stream does not exist");
        
        return await _chatRepository.GetMessagesByStreamIdAsync(request.StreamId, cancellationToken);
    }
}