using AutoMapper;
using SampleCrud_ASPNET.Models.Dtos.Auth;
using SampleCrud_ASPNET.Models.Entities;

namespace SampleCrud_ASPNET.MappingProfiles;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<AuthRegisterUserDto, User>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
    }
}
