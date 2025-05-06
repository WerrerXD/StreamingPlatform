using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.StreamHandlers;

public class CreateStreamHandler : IRequestHandler<CreateStreamCommand>
{
    private readonly IStreamRepository _repository;
    private readonly IStreamCategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateStreamHandler(IStreamRepository repository, IMapper mapper, IStreamCategoryRepository categoryRepository, ILoggingService loggingService)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _loggingService = loggingService;
    }

    public async Task Handle(CreateStreamCommand request, CancellationToken cancellationToken)
    {
        var streams = await _repository.GetAllByStreamerAsync(request.StreamerId, cancellationToken);
        
        if (streams.Any(testStream => testStream.EndTime == null))
        {
            throw new AlreadyExistsException("You are already streaming");
        }
        
        var category = await _categoryRepository.GetStreamCategoryByIdAsync(request.CategoryId, cancellationToken)
            ?? throw new NotFoundException("Category with this id does not exist");
        
        var stream = _mapper.Map<StreamModel>(request);

        await _repository.CreateAsync(stream, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Created stream with id: {stream.Id}");
    }
}