using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.Presentation.Controllers;

[ApiController]
[Route("stream-service/chat")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;

    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages(string streamId)
    {
        var messages = await _mediator.Send(new GetChatMessagesQuery(streamId));
        return Ok(messages);
    }

    [HttpPost("message")]
    public async Task<IActionResult> SendMessage([FromBody] CreateChatMessageCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }
}