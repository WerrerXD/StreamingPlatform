using AutoMapper;
using Stream.Service.BusinessLogic.Commands;
using Stream.Service.Domain.Models;

namespace Stream.Service.BusinessLogic.Mappings;

public class TestProfile: Profile
{
    public TestProfile() 
    {
        CreateMap<CreateStreamCategoryCommand, StreamCategory>();

        CreateMap<CreateStreamCommand, StreamModel>();

        CreateMap<CreateChatMessageCommand, ChatMessage>();
    }
}