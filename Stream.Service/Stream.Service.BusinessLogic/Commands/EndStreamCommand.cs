using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record EndStreamCommand(
    string StreamId
    ) : IRequest;