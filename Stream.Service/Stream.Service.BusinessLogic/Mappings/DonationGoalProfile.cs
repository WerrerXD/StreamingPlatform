using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class DonationGoalProfile : Profile
{
    public DonationGoalProfile()
    {
        CreateMap<CreateDonationGoalCommand, DonationGoal>()
            .ForMember(dest => dest.StreamerId, opt => opt.MapFrom(src => src.StreamerId))
            .ForMember(dest => dest.TargetAmount, opt => opt.MapFrom(src => src.Dto.TargetAmount))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Dto.Title));
    }
}