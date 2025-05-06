using MediatR;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.DonationHandlers;

public class GetAllStreamerDonationGoalsHandler: IRequestHandler<GetAllStreamerDonationGoalsQuery, List<DonationGoal>>
{
    private readonly IDonationGoalRepository _donationGoalRepository;

    public GetAllStreamerDonationGoalsHandler(IDonationGoalRepository donationGoalRepository)
    {
        _donationGoalRepository = donationGoalRepository;
    }

    public async Task<List<DonationGoal>> Handle(GetAllStreamerDonationGoalsQuery request, CancellationToken cancellationToken)
    {
        return await _donationGoalRepository.GetAllDonationGoalsByStreamerIdAsync(request.StreamerId, cancellationToken);
    }
}