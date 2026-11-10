using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StepITFinalProject.Data;
using StepITFinalProject.Common;
using StepITFinalProject.Data;
using StepITFinalProject.Services.Interfaces;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Services.Implementations;

public class AuthorService : IAuthorService
{
    private readonly Baza _baza;
    private readonly IMapper _mapper;

    public AuthorService(Baza baza, IMapper mapper)
    {
        _baza = baza;
        _mapper = mapper;
    }

    public ApiResponse<List<GetAuthorResponse>> GetAllAuthors()
    {
        var allAuthors = _baza.Authors.ToList();
        var result = _mapper.Map<List<GetAuthorResponse>>(allAuthors);
        return ApiResponse<List<GetAuthorResponse>>.OK(result);

    }

    public ApiResponse <GetAuthorDetailsResponse> GetAuthorByIdWithBooks(int id)
    {
        var author = _baza.Authors
            .Include(a => a.Books)
                //.ThenInclude(b => b.Genres)
            .FirstOrDefault(a => a.Id == id);

        if(author == null)
        {
            return ApiResponse<GetAuthorDetailsResponse>.Fail(404, "Author not found");
        }


        return ApiResponse<GetAuthorDetailsResponse>.OK(_mapper.Map<GetAuthorDetailsResponse>(author));
    }

    public ApiResponse<CreateAuthorResponse> CreateAuthor(CreateAuthorRequest request)
    {
        bool nameTaken = _baza.Authors.Any(a => a.FirstName.ToLower() == request.FirstName.ToLower() && a.LastName.ToLower() == request.LastName.ToLower());

        if(nameTaken)
        {
            return ApiResponse<CreateAuthorResponse>.Fail(409, "Author with this name already exists");
        }

        var author = _mapper.Map<Author>(request);
        _baza.Authors.Add(author);
        _baza.SaveChanges();

        var response = _mapper.Map<CreateAuthorResponse>(author);
        return ApiResponse<CreateAuthorResponse>.OK(response, $"Author {author.Id} created successfully", 201);
    }

    public ApiResponse<GetAuthorResponse> UpdateAuthor(int id, UpdateAuthorRequest request)
    {
        var author = _baza.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return ApiResponse<GetAuthorResponse>.Fail(404, "Author not found");
        }

        bool nameTaken = _baza.Authors.Any(a => a.Id != id && a.FirstName.ToLower() == request.FirstName.ToLower() && a.LastName.ToLower() == request.LastName.ToLower());
        if (nameTaken)
        {
            return ApiResponse<GetAuthorResponse>.Fail(409, "Author with this name already exists");
        }

        _mapper.Map(request, author);
        _baza.SaveChanges();
        var response = _mapper.Map<GetAuthorResponse>(author);
        return ApiResponse<GetAuthorResponse>.OK(response, $"Author {author.Id} updated successfully");
    }

    public ApiResponse<bool> DeleteAuthor(int id)
    {
        var author = _baza.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            return ApiResponse<bool>.Fail(404, "Author not found");
        }

        bool hasBooks = _baza.Books.Any(b => b.AuthorId == id);
        if (hasBooks)
        {
            return ApiResponse<bool>.Fail(400, "Author has books and cannot be deleted");
        }

        _baza.Authors.Remove(author);
        _baza.SaveChanges();
        return ApiResponse<bool>.OK(true, $"Author {author.Id} deleted successfully");
    }

}
