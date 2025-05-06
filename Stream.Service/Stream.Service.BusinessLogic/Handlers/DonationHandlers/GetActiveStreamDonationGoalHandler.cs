using MediatR;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.DonationHandlers;

public class GetActiveStreamDonationGoalHandler: IRequestHandler<GetActiveStreamDonationGoalQuery, DonationGoal>
{
    private readonly IDonationGoalRepository _donationGoalRepository;
    private readonly IStreamRepository _streamRepository;

    public GetActiveStreamDonationGoalHandler(IDonationGoalRepository donationGoalRepository, IStreamRepository streamRepository)
    {
        _donationGoalRepository = donationGoalRepository;
        _streamRepository = streamRepository;
    }
    
    public async Task<DonationGoal> Handle(GetActiveStreamDonationGoalQuery request, CancellationToken cancellationToken)
    {
        var stream = await _streamRepository.GetByIdAsync(request.StreamId, cancellationToken) 
            ?? throw new NotFoundException("Stream does not exist");
        
        var donationGoal = await _donationGoalRepository.GetActiveDonationGoalByStreamerIdAsync(stream.StreamerId, cancellationToken)
            ?? throw new NotFoundException("No active donation goal was found");
        
        return donationGoal;
    }
}