using AutoMapper;
using StepITFinalProject.Requests;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        CreateMap<Author, GetAuthorResponse>();
        CreateMap<Author, CreateAuthorResponse>();
        CreateMap<Author, GetAuthorDetailsResponse>();

        CreateMap<Book, AuthorBooks>()
            .ForMember(d => d.PublicationYear, o => o.MapFrom(s => s.PublicationYear))
            .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(", ", s.Genres.Select(g => g.Name))));

        CreateMap<CreateAuthorRequest, Author>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Books, o => o.Ignore());

        CreateMap<UpdateAuthorRequest, Author>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Books, o => o.Ignore());
    }
}

