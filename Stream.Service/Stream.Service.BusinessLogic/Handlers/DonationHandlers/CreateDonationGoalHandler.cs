using AutoMapper;
using MediatR;
using Stream.Service.BusinessLogic.Abstractions;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.BusinessLogic.Exceptions;
using Stream.Service.Domain.Interfaces;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Handlers.DonationHandlers;

public class CreateDonationGoalHandler: IRequestHandler<CreateDonationGoalCommand>
{
    private readonly IDonationGoalRepository _donationGoalRepository;
    private readonly IMapper _mapper;
    private readonly ILoggingService _loggingService;

    public CreateDonationGoalHandler(IDonationGoalRepository donationGoalRepository, IMapper mapper, ILoggingService loggingService)
    {
        _donationGoalRepository = donationGoalRepository;
        _mapper = mapper;
        _loggingService = loggingService;
    }

    public async Task Handle(CreateDonationGoalCommand request, CancellationToken cancellationToken)
    {
        var activeDonationGoal = await _donationGoalRepository.GetActiveDonationGoalByStreamerIdAsync(request.StreamerId, cancellationToken);

        if (activeDonationGoal != null)
        {
            throw new AlreadyExistsException("You already have an active donation goal.");
        }
        
        var donationGoal = _mapper.Map<DonationGoal>(request);
        
        await _donationGoalRepository.CreateAsync(donationGoal, cancellationToken);
        
        await _loggingService.LogInformationAsync($"Created donation-goal with id: {donationGoal.Id}");
    }
}