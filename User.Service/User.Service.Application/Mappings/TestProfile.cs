using User.Service.Domain.Entities;
using User.Service.Shared.DTO;
using Profile = AutoMapper.Profile;

namespace User.Service.Application.Mappings;

public class TestProfile: Profile
{
    public TestProfile() 
    {
        CreateMap<AppUser, UserDTO>();

        CreateMap<Report, ReportDTO>();
    }
}