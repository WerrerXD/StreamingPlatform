using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.StreamCategoryHandlers;

public class CreateStreamCategoryHandler: IRequestHandler<CreateStreamCategoryCommand>
{
    private readonly IStreamCategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateStreamCategoryHandler(IStreamCategoryRepository repository, IMapper mapper, ILoggingService loggingService)
    {
        _repository = repository;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task Handle(CreateStreamCategoryCommand request, CancellationToken cancellationToken)
    {
        var streamCategory = _mapper.Map<StreamCategory>(request);

        await _repository.CreateAsync(streamCategory, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Created stream-category with id: {streamCategory.Id}");
    }
}