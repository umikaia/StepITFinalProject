using AutoMapper;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
namespace StepITFinalProject.Mapping;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterRequest, User>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.HashedPassword, o => o.Ignore())   
            .ForMember(d => d.Role, o => o.Ignore())             
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.LibraryCard, o => o.Ignore())
            .ForMember(d => d.BorrowRecords, o => o.Ignore());
    }
}
