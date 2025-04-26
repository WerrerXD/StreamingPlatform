using MediatR;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Commands;

public record CreateDonationGoalCommand(
    string StreamerId,
    CreateDonationGoalDto Dto
    ) : IRequest;