using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record CloseActiveDonationGoalCommand(
    string StreamerId
    ) : IRequest;