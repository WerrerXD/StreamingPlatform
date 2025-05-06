using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class ChatMessageProfile : Profile
{
    public ChatMessageProfile()
    {
        CreateMap<CreateChatMessageCommand, ChatMessage>()
            .ForMember(dest => dest.StreamId, opt => opt.MapFrom(src => src.StreamId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Dto.UserId))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Dto.Message));
    }
}