using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class StreamProfile : Profile
{
    public StreamProfile()
    {
        CreateMap<CreateStreamCommand, StreamModel>();
    }
}