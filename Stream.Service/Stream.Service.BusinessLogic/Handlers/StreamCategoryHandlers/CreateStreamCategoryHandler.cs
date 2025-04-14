using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.StreamCategoryHandlers;

public class CreateStreamCategoryHandler: IRequestHandler<CreateStreamCategoryCommand, string>
{
    private readonly IStreamCategoryRepository _repository;
    private readonly IMapper _mapper;

    public CreateStreamCategoryHandler(IStreamCategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateStreamCategoryCommand request, CancellationToken cancellationToken)
    {
        var streamCategory = _mapper.Map<StreamCategory>(request);

        return await _repository.CreateAsync(streamCategory, cancellationToken);
    }
}