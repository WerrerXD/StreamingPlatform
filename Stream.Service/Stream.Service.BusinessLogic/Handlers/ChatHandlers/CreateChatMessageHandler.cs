using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.ChatHandlers;

public class CreateChatMessageHandler : IRequestHandler<CreateChatMessageCommand>
{
    private readonly IChatRepository _chatRepository;
    private readonly IStreamRepository _streamRepository;
    private readonly IChatNotificationService _notificationService;
    private readonly IMapper _mapper;

    public CreateChatMessageHandler(IChatRepository chatRepository, IChatNotificationService notificationService, IMapper mapper, IStreamRepository streamRepository)
    {
        _chatRepository = chatRepository;
        _notificationService = notificationService;
        _mapper = mapper;
        _streamRepository = streamRepository;
    }

    public async Task Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
    {
        var stream = await _streamRepository.GetByIdAsync(request.StreamId, cancellationToken)
                     ?? throw new NotFoundException("Stream does not exist");

        if (stream.EndTime != null)
        {
            throw new BadRequestException("You cannot change stream that already ended");
        }
        
        var message = _mapper.Map<ChatMessage>(request);
        
        await _chatRepository.AddMessageAsync(message, cancellationToken);
        
        await _notificationService.SendMessageToGroupAsync(request.StreamId, message);
    }
}