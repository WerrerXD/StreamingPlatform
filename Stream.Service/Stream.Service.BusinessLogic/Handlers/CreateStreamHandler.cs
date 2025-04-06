using MediatR;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers;

public class CreateStreamHandler : IRequestHandler<CreateStreamCommand, string>
{
    private readonly IStreamRepository _repository;

    public CreateStreamHandler(IStreamRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> Handle(CreateStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = new StreamModel
        {
            StreamerId = request.StreamerId,
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            StartTime = DateTime.UtcNow,
            ViewersCount = 0
        };

        return await _repository.CreateAsync(stream);
    }
}