using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers;

public class CreateStreamHandler : IRequestHandler<CreateStreamCommand, string>
{
    private readonly IStreamRepository _repository;
    private readonly IStreamCategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateStreamHandler(IStreamRepository repository, IMapper mapper, IStreamCategoryRepository categoryRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public async Task<string> Handle(CreateStreamCommand request, CancellationToken cancellationToken)
    {
        var categoryExists = await _categoryRepository.GetStreamCategoryById(request.CategoryId, cancellationToken)
            ?? throw new Exception("Category with this id does not exist");
        
        var stream = _mapper.Map<StreamModel>(request);

        return await _repository.CreateAsync(stream, cancellationToken);
    }
}