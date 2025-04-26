using MediatR;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Queries;

public record GetAllStreamerDonationsQuery(
    string StreamerId
    ) : IRequest<List<Donation>>;