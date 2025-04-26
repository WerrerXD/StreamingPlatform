using MediatR;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Commands;

public record DonateStreamerCommand(
    string StreamId,
    DonateStreamerDto Dto
    ) : IRequest;