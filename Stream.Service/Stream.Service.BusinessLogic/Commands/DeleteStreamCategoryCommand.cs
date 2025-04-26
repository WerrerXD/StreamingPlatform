using MediatR;

namespace Stream.Service.BusinessLogic.Commands;

public record DeleteStreamCategoryCommand(
    string CategoryId
    ) : IRequest;