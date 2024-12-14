using AutoMapper;
using SampleCrud_ASPNET.Models.Dtos.Users;
using SampleCrud_ASPNET.Models.Entities;

namespace SampleCrud_ASPNET.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDetailsDto>();
    }
}
