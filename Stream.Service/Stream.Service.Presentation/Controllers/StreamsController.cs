using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Contracts;
using Stream.Service.BusinessLogic.Queries;

namespace Stream.Service.Presentation.Controllers;

[ApiController]
[Route("stream-service/streams")]
public class StreamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StreamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStream([FromBody] CreateStreamCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }

    [HttpGet("{streamId}")]
    public async Task<IActionResult> GetStreamById([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        var stream = await _mediator.Send(new GetStreamByIdQuery(streamId), cancellationToken);
        
        return Ok(stream);
    }

    [HttpGet("{streamerId}/streams")]
    public async Task<IActionResult> GetStreamsByStreamer([FromRoute] string streamerId, CancellationToken cancellationToken)
    {
        var streams = await _mediator.Send(new GetAllStreamerStreamsQuery(streamerId), cancellationToken);
        
        return Ok(streams);
    }

    [HttpPut("{streamId}/end")]
    public async Task<IActionResult> EndStream([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new EndStreamCommand(streamId), cancellationToken);
        
        return Ok();
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveStreams(CancellationToken cancellationToken)
    {
        var streams = await _mediator.Send(new GetAllActiveStreamsQuery(), cancellationToken);
        
        return Ok(streams);
    }

    [HttpPut]
    public async Task<IActionResult> ChangeStream([FromForm] ChangeStreamCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("{streamId}/chat-messages")]
    public async Task<IActionResult> GetMessages([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        var messages = await _mediator.Send(new GetChatMessagesQuery(streamId), cancellationToken);
        
        return Ok(messages);
    }

    [HttpPost("{streamId}/chat-messages")]
    public async Task<IActionResult> SendMessage([FromRoute] string streamId, [FromBody] CreateChatMessageDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateChatMessageCommand(streamId, dto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }

    [HttpPost("{streamId}/donations")]
    public async Task<IActionResult> Donate([FromRoute] string streamId, [FromBody] DonateStreamerDto commandDto, CancellationToken cancellationToken)
    {
        var command = new DonateStreamerCommand(streamId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("{streamId}/donation-goals/active")]
    public async Task<IActionResult> GetActiveDonationGoal([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        var donationGoal = await _mediator.Send(new GetActiveStreamDonationGoalQuery(streamId), cancellationToken);
        
        return Ok(donationGoal);
    }
}