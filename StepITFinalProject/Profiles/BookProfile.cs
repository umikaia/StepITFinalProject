using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StepITFinalProject.Data;
using StepITFinalProject.Requests;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {

        CreateMap<Book, GetBookResponse>()
            .ForMember(d => d.AuthorName, o => o.MapFrom(s => s.Author.FirstName + " " + s.Author.LastName))
            .ForMember(d => d.Genres, o => o.MapFrom(s => s.Genres.Select(g => g.Name)))
            .ForMember(d => d.IsAvailable, o => o.MapFrom(s => s.AvailableCopies > 0));

        CreateMap<Book, GetBookDetailsResponse>()
            .ForMember(d => d.PublishedYear,
                o => o.MapFrom(s => s.PublicationYear))
            .ForMember(d => d.AuthorName,
                o => o.MapFrom(s => s.Author.FirstName + " " + s.Author.LastName))
            .ForMember(d => d.Genres,
                o => o.MapFrom(s => s.Genres.Select(g => g.Name)))
            .ForMember(d => d.IsAvailable,
                o => o.MapFrom(s => s.AvailableCopies > 0));

        CreateMap<Book, CreateBookResponse>();

        CreateMap<CreateBookRequest, Book>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.AvailableCopies, o => o.MapFrom(s => s.TotalCopies))
            .ForMember(d => d.Author, o => o.Ignore())
            .ForMember(d => d.Genres, o => o.Ignore())
            .ForMember(d => d.BorrowRecords, o => o.Ignore());

        CreateMap<UpdateBookRequest, Book>()
            .ForMember(d => d.PublicationYear,
                o => o.MapFrom(s => s.PublishedYear))
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.AvailableCopies, o => o.Ignore())
            .ForMember(d => d.Author, o => o.Ignore())
            .ForMember(d => d.Genres, o => o.Ignore())
            .ForMember(d => d.BorrowRecords, o => o.Ignore());
    }
}
