using AutoMapper;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class LibraryCardProfile : Profile
{
    public LibraryCardProfile()
    {
        CreateMap<LibraryCard, GetLibraryCardResponse>()
            .ForMember(d => d.IsActive, o => o.MapFrom(s => s.ExpirationDate > DateTime.UtcNow));
    }
}
