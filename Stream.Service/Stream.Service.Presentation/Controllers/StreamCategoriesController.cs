using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
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
    
    [HttpPut]
    public async Task<IActionResult> ChangeStreamCategory([FromForm] ChangeStreamCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteStreamCategory([FromForm] DeleteStreamCategoryCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
}