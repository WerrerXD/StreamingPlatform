using User.Service.Application.Contracts;
using User.Service.Domain.Entities;
using Profile = AutoMapper.Profile;

namespace User.Service.Application.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile() 
    {
        CreateMap<AppUser, UserDto>();

        CreateMap<Report, ReportDto>();
    }
}