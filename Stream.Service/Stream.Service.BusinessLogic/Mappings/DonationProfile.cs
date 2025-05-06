using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class DonationProfile : Profile
{
    public DonationProfile()
    {
        CreateMap<DonateStreamerCommand, Donation>()
            .ForMember(dest => dest.StreamId, opt => opt.MapFrom(src => src.StreamId))
            .ForMember(dest => dest.DonorId, opt => opt.MapFrom(src => src.Dto.DonorId))
            .ForMember(dest => dest.DonorName, opt => opt.MapFrom(src => src.Dto.DonorName))
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Dto.Amount))
            .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Dto.Message));
    }
}