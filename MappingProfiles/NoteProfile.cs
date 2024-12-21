using AutoMapper;
using SampleCrud_ASPNET.Models.Dtos.Notes;
using SampleCrud_ASPNET.Models.Entities;

namespace SampleCrud_ASPNET.MappingProfiles;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<Note, NoteDto>();
        CreateMap<UpdateNoteDto, Note>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<CreateNoteDto, Note>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                if (context.Items.TryGetValue("User", out var userObj) && userObj is User user)
                {
                    dest.User = user;
                    dest.UserId = user.Id;
                }
            });

    }
}
