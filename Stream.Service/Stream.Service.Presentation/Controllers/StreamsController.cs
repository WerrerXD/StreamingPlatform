using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
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
    public async Task<IActionResult> CreateStream([FromBody] CreateStreamCommand command)
    {
        var streamId = await _mediator.Send(command);
        return Ok(new { id = streamId });
    }

    [HttpGet]
    public async Task<IActionResult> GetStreamById(string streamId)
    {
        var stream = await _mediator.Send(new GetStreamByIdQuery(streamId));
        return Ok(stream);
    }
}