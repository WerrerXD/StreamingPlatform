using MediatR;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.StreamCategoryHandlers;

public class GetAllStreamCategoriesHandler : IRequestHandler<GetAllStreamCategoriesQuery, List<StreamCategory>>
{
    private readonly IStreamCategoryRepository _repository;

    public GetAllStreamCategoriesHandler(IStreamCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<StreamCategory>> Handle(GetAllStreamCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}