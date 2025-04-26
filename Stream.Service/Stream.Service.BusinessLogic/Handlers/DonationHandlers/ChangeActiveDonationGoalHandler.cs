using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers.DonationHandlers;

public class ChangeActiveDonationGoalHandler: IRequestHandler<ChangeActiveDonationGoalCommand>
{
    private readonly IDonationGoalRepository _donationGoalRepository;
    private readonly ILoggingService _loggingService;

    public ChangeActiveDonationGoalHandler(IDonationGoalRepository donationGoalRepository, ILoggingService loggingService)
    {
        _donationGoalRepository = donationGoalRepository;
        _loggingService = loggingService;
    }

    public async Task Handle(ChangeActiveDonationGoalCommand request, CancellationToken cancellationToken)
    {
        var donationGoal = await _donationGoalRepository.GetActiveDonationGoalByStreamerIdAsync(request.StreamerId, cancellationToken)
            ?? throw new NotFoundException("No active donation goal was found");
        
        if (!string.IsNullOrEmpty(request.Dto.Title))
        {
            donationGoal.Title = request.Dto.Title;
        }

        if (request.Dto.TargetAmount.HasValue)
        {
            donationGoal.TargetAmount = request.Dto.TargetAmount.Value;
        }
        
        await _donationGoalRepository.UpdateAsync(donationGoal, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Donation-goal with id: {donationGoal.Id} was successfully updated");
    }
}