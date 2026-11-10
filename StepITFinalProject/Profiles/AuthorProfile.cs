using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Data;
using StepITFinalProject.Requests;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {

        CreateMap<Author, GetAuthorResponse>();
        CreateMap<Author, GetAuthorDetailsResponse>();
        CreateMap<Author, CreateAuthorResponse>();

        CreateMap<Book, AuthorBooks>();


        CreateMap<UpdateAuthorRequest, Author>();
    }
}
