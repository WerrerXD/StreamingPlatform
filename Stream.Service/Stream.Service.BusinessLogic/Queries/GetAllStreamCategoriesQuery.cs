using MediatR;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Queries;

public record GetAllStreamCategoriesQuery()
    : IRequest<List<StreamCategory>>;