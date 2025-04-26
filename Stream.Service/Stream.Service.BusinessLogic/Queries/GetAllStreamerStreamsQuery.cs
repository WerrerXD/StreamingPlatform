using MediatR;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Queries;

public record GetAllStreamerStreamsQuery(
    string StreamerId
    ) : IRequest<List<StreamModel>>;