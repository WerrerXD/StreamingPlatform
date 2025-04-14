using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Models;

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
    public async Task<IActionResult> CreateStream([FromBody] CreateStreamCommand command,
        CancellationToken cancellationToken)
    {
        var streamId = await _mediator.Send(command, cancellationToken);
        return Ok(new { id = streamId });
    }

    [HttpGet("{streamId}")]
    public async Task<IActionResult> GetStreamById(string streamId, CancellationToken cancellationToken)
    {
        var stream = await _mediator.Send(new GetStreamByIdQuery(streamId), cancellationToken);
        return Ok(stream);
    }

    [HttpGet("{streamerId}/streams")]
    public async Task<IActionResult> GetStreamsByStreamer(string streamerId, CancellationToken cancellationToken)
    {
        var streams = await _mediator.Send(new GetAllStreamerStreamsQuery(streamerId), cancellationToken);
        return Ok(streams);
    }

    [HttpPut("{streamId}/end")]
    public async Task<IActionResult> EndStream(string streamId, CancellationToken cancellationToken)
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
    
    
}