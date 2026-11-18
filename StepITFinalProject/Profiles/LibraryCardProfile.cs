using AutoMapper;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class LibraryCardProfile : Profile
{
    public LibraryCardProfile()
    {
        CreateMap<LibraryCard, GetLibraryCardResponse>()
    .ForMember(d => d.IssuedAt,
        o => o.MapFrom(s => s.IsuedAt))
    .ForMember(d => d.ExpiresAt,
        o => o.MapFrom(s => s.ExpirationDate))
    .ForMember(d => d.IsActive,
        o => o.MapFrom(s => s.ExpirationDate > DateTime.UtcNow));
    }
}
