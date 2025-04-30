using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class StreamCategoryProfile : Profile
{
    public StreamCategoryProfile()
    {
        CreateMap<CreateStreamCategoryCommand, StreamCategory>();
    }
}