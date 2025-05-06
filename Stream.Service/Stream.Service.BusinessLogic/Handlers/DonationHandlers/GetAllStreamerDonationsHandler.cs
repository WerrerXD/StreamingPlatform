using MediatR;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.BusinessLogic.Queries;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.DonationHandlers;

public class GetAllStreamerDonationsHandler: IRequestHandler<GetAllStreamerDonationsQuery, List<Donation>>
{
    private readonly IDonationRepository _donationRepository;
    private readonly IStreamRepository _streamRepository;

    public GetAllStreamerDonationsHandler(IDonationRepository donationRepository, IStreamRepository streamRepository)
    {
        _donationRepository = donationRepository;
        _streamRepository = streamRepository;
    }

    public async Task<List<Donation>> Handle(GetAllStreamerDonationsQuery request, CancellationToken cancellationToken)
    {
        var streams = await _streamRepository.GetAllByStreamerAsync(request.StreamerId, cancellationToken)
                     ?? throw new NotFoundException("You dont have any streams yet");
        
        var streamerDonations = new List<Donation>();

        foreach (var stream in streams)
        {
            var streamDonations = await _donationRepository.GetDonationsByStreamIdAsync(stream.Id, cancellationToken);

            streamerDonations.AddRange(streamDonations);
        }
        
        return streamerDonations;
    }
}