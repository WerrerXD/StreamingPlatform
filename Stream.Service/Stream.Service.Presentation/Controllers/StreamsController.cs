using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Contracts;
using Stream.Service.BusinessLogic.Queries;

namespace Stream.Service.Presentation.Controllers;

[ApiController]
[Route("stream-service")]
public class StreamsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StreamsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("streams")]
    public async Task<IActionResult> CreateStream([FromBody] CreateStreamCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }

    [HttpGet("streams/{streamId}")]
    public async Task<IActionResult> GetStreamById([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        var stream = await _mediator.Send(new GetStreamByIdQuery(streamId), cancellationToken);
        
        return Ok(stream);
    }

    [HttpGet("streamers/{streamerId}/streams")]
    public async Task<IActionResult> GetStreamsByStreamer([FromRoute] string streamerId, CancellationToken cancellationToken)
    {
        var streams = await _mediator.Send(new GetAllStreamerStreamsQuery(streamerId), cancellationToken);
        
        return Ok(streams);
    }

    [HttpPatch("streams/{streamId}/status")]
    public async Task<IActionResult> EndStream([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new EndStreamCommand(streamId), cancellationToken);
        
        return Ok();
    }

    [HttpGet("streams/active")]
    public async Task<IActionResult> GetActiveStreams(CancellationToken cancellationToken)
    {
        var streams = await _mediator.Send(new GetAllActiveStreamsQuery(), cancellationToken);
        
        return Ok(streams);
    }

    [HttpPut("streams/{streamId}")]
    public async Task<IActionResult> ChangeStream([FromRoute] string streamId, [FromForm] ChangeStreamDto commandDto, CancellationToken cancellationToken)
    {
        var command = new ChangeStreamCommand(streamId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("streams/{streamId}/chat-messages")]
    public async Task<IActionResult> GetMessages([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        var messages = await _mediator.Send(new GetChatMessagesQuery(streamId), cancellationToken);
        
        return Ok(messages);
    }

    [HttpPost("streams/{streamId}/chat-messages")]
    public async Task<IActionResult> SendMessage([FromRoute] string streamId, [FromBody] CreateChatMessageDto commandDto, CancellationToken cancellationToken)
    {
        var command = new CreateChatMessageCommand(streamId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }

    [HttpPost("streams/{streamId}/donations")]
    public async Task<IActionResult> Donate([FromRoute] string streamId, [FromBody] DonateStreamerDto commandDto, CancellationToken cancellationToken)
    {
        var command = new DonateStreamerCommand(streamId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("streams/{streamId}/donation-goals/active")]
    public async Task<IActionResult> GetActiveDonationGoal([FromRoute] string streamId, CancellationToken cancellationToken)
    {
        var donationGoal = await _mediator.Send(new GetActiveStreamDonationGoalQuery(streamId), cancellationToken);
        
        return Ok(donationGoal);
    }
}