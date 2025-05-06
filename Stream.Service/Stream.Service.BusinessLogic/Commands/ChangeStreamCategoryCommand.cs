using MediatR;
using Stream.Service.BusinessLogic.Contracts;

namespace Stream.Service.BusinessLogic.Commands;

public record ChangeStreamCategoryCommand(
    string Id,
    ChangeStreamCategoryDto Dto
    ) : IRequest;