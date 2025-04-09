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
    public async Task<IActionResult> CreateStream([FromBody] CreateStreamCommand command, CancellationToken cancellationToken)
    {
        var streamId = await _mediator.Send(command, cancellationToken);
        return Ok(new { id = streamId });
    }

    [HttpGet]
    public async Task<IActionResult> GetStreamById(string streamId, CancellationToken cancellationToken)
    {
        var stream = await _mediator.Send(new GetStreamByIdQuery(streamId), cancellationToken);
        return Ok(stream);
    }

    [HttpPost("categories")]
    public async Task<IActionResult> CreateStreamCategory([FromBody] CreateStreamCategoryCommand command, CancellationToken cancellationToken)
    {
        var streamCategoryId = await _mediator.Send(command, cancellationToken);
        return Ok(new { id = streamCategoryId });
    }
    
    [HttpGet("categories")]
    public async Task<IActionResult> GetAllStreamCategories(CancellationToken cancellationToken)
    {
        var streamCategories = await _mediator.Send(new GetAllStreamCategoriesQuery(), cancellationToken);
        return Ok(streamCategories);
    }
    
    [HttpPut("categories")]
    public async Task<IActionResult> ChangeStreamCategory([FromForm] ChangeStreamCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("categories")]
    public async Task<IActionResult> DeleteStreamCategory([FromForm] DeleteStreamCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("{streamerId}")]
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
}