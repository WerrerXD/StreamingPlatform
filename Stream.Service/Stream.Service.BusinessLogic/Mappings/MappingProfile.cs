using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile() 
    {
        CreateMap<CreateStreamCategoryCommand, StreamCategory>();

        CreateMap<CreateStreamCommand, StreamModel>();

        CreateMap<CreateChatMessageCommand, ChatMessage>()
            .ForMember(dest => dest.StreamId, opt => opt.MapFrom(src => src.StreamId))
            .ForMember(dest => dest.UserId, opt=> opt.MapFrom(src => src.Dto.UserId))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Dto.Message));

        CreateMap<DonateStreamerCommand, Donation>()
            .ForMember(dest => dest.StreamId, opt => opt.MapFrom(src => src.StreamId))
            .ForMember(dest => dest.DonorId, opt=> opt.MapFrom(src => src.Dto.DonorId))
            .ForMember(dest => dest.DonorName, opt=> opt.MapFrom(src => src.Dto.DonorName))
            .ForMember(dest => dest.Amount, opt=> opt.MapFrom(src => src.Dto.Amount))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Dto.Message));

        CreateMap<CreateDonationGoalCommand, DonationGoal>()
            .ForMember(dest => dest.StreamerId, opt => opt.MapFrom(src => src.StreamerId))
            .ForMember(dest => dest.TargetAmount, opt => opt.MapFrom(src => src.Dto.TargetAmount))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Dto.Title));
    }
}