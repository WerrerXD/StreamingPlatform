using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.DonationHandlers;

public class DonateStreamerHandler: IRequestHandler<DonateStreamerCommand>
{
    private readonly IDonationRepository _donationRepository;
    private readonly IStreamRepository _streamRepository;
    private readonly IPaymentService _paymentService;
    private readonly IDonationGoalRepository _donationGoalRepository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public DonateStreamerHandler(IDonationRepository donationRepository, IPaymentService paymentService, IMapper mapper, IStreamRepository streamRepository, IDonationGoalRepository donationGoalRepository, ILoggingService loggingService)
    {
        _donationRepository = donationRepository;
        _paymentService = paymentService;
        _mapper = mapper;
        _streamRepository = streamRepository;
        _donationGoalRepository = donationGoalRepository;
        _loggingService = loggingService;
    }

    public async Task Handle(DonateStreamerCommand request, CancellationToken cancellationToken)
    {
        var stream = await _streamRepository.GetByIdAsync(request.StreamId, cancellationToken)
            ?? throw new NotFoundException("Stream does not exist");

        if (stream.EndTime != null)
        {
            throw new BadRequestException("You cannot donate to stream that already ended");
        }
        
        var paymentSuccess = await _paymentService.ProcessPaymentAsync(request.Dto.DonorId, request.Dto.Amount);
        
        if (!paymentSuccess)
        {
            await _loggingService.LogInformationAsync($"Payment failed for {request.Dto.DonorId} in amount of {request.Dto.Amount}");
            
            throw new BadRequestException("Payment failed.");
        }
        
        await _loggingService.LogInformationAsync($"Payment successful for {request.Dto.DonorId} in amount of {request.Dto.Amount}");
        
        var donation = _mapper.Map<Donation>(request);
        
        await _donationRepository.CreateAsync(donation, cancellationToken);
        
        var donationGoal = await _donationGoalRepository.GetActiveDonationGoalByStreamerIdAsync(stream.StreamerId, cancellationToken);

        if (donationGoal != null)
        {
            donationGoal.CollectedAmount += request.Dto.Amount;
            
            await _donationGoalRepository.UpdateAsync(donationGoal, cancellationToken);
        }
        
        await _loggingService.LogInformationAsync($"Donation {donation.Id} has been successfully processed");
    }
}