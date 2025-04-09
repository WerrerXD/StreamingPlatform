using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;

namespace Stream.Service.BusinessLogic.Handlers;

public class DeleteStreamCategoryHandler: IRequestHandler<DeleteStreamCategoryCommand>
{
    private readonly IStreamCategoryRepository _repository;

    public DeleteStreamCategoryHandler(IStreamCategoryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(DeleteStreamCategoryCommand request, CancellationToken cancellationToken)
    {
        var streamCategory = await _repository.GetStreamCategoryById(request.CategoryId, cancellationToken) 
            ?? throw new Exception("Stream category does not exist");
        await _repository.DeleteStreamCategory(streamCategory, cancellationToken);
    }
}