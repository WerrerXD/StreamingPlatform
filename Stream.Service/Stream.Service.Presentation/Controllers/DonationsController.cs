using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Contracts;
using Stream.Service.BusinessLogic.Queries;

namespace Stream.Service.Presentation.Controllers;

[ApiController]
[Route("stream-service/donations/{streamerId}")]
public class DonationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DonationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("donations")]
    public async Task<IActionResult> GetDonations([FromRoute] string streamerId, CancellationToken cancellationToken)
    {
        var donations = await _mediator.Send(new GetAllStreamerDonationsQuery(streamerId), cancellationToken);
        
        return Ok(donations);
    }
    
    [HttpPost("donation-goals")]
    public async Task<IActionResult> CreateDonationGoal([FromRoute] string streamerId, [FromBody] CreateDonationGoalDto commandDto, CancellationToken cancellationToken)
    {
        var command = new CreateDonationGoalCommand(streamerId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpPut("donation-goals/active")]
    public async Task<IActionResult> ChangeActiveDonationGoal([FromRoute] string streamerId, [FromBody] ChangeActiveDonationGoalDto commandDto, CancellationToken cancellationToken)
    {
        var command = new ChangeActiveDonationGoalCommand(streamerId, commandDto);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok();
    }
    
    [HttpDelete("donation-goals/active")]
    public async Task<IActionResult> CloseActiveDonationGoal([FromRoute] string streamerId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CloseActiveDonationGoalCommand(streamerId), cancellationToken);
        
        return Ok();
    }
    
    [HttpGet("donation-goals")]
    public async Task<IActionResult> GetAllStreamerDonationGoals([FromRoute] string streamerId, CancellationToken cancellationToken)
    {
        var donationGoals = await _mediator.Send(new GetAllStreamerDonationGoalsQuery(streamerId), cancellationToken);
        
        return Ok(donationGoals);
    }
}