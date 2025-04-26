using MediatR;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Queries;

public record GetAllStreamerDonationGoalsQuery(
    string StreamerId
    ) : IRequest<List<DonationGoal>>;