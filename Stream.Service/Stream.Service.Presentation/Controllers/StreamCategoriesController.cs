using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Contracts;
using Stream.Service.BusinessLogic.Queries;

namespace Stream.Service.Presentation.Controllers;

[ApiController]
[Route("stream-service/stream-categories")]
public class StreamCategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public StreamCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStreamCategory([FromBody] CreateStreamCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStreamCategories(CancellationToken cancellationToken)
    {
        var streamCategories = await _mediator.Send(new GetAllStreamCategoriesQuery(), cancellationToken);
        
        return Ok(streamCategories);
    }
    
    [HttpPut("{streamCategoryId}")]
    public async Task<IActionResult> ChangeStreamCategory([FromRoute] string streamCategoryId, [FromForm] ChangeStreamCategoryDto commandDto, CancellationToken cancellationToken)
    {
        var command = new ChangeStreamCategoryCommand(streamCategoryId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpDelete("{streamCategoryId}")]
    public async Task<IActionResult> DeleteStreamCategory([FromRoute] string streamCategoryId, CancellationToken cancellationToken)
    {
        var command = new DeleteStreamCategoryCommand(streamCategoryId);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
}