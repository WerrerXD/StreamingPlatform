using MediatR;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Queries;

public record GetActiveStreamDonationGoalQuery(
    string StreamId
    ) : IRequest<DonationGoal>;