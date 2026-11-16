using AutoMapper;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GetGenreResponse>();
        CreateMap<Genre, CreateGenreResponse>();

        CreateMap<CreateGenreRequest, Genre>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Books, o => o.Ignore());
    }
}
