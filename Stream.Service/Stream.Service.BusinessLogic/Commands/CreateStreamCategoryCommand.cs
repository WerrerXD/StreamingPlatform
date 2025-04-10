using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record CreateStreamCategoryCommand(
    string Name,
    string Description
    ): IRequest<string>;