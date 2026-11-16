using AutoMapper;
using StepITFinalProject.Services.Interfaces;
using StepITFinalProject.Common;
using StepITFinalProject.Data;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;

namespace StepITFinalProject.Services.Implementations; 

public class GenreService : IGenreService
{
    private readonly Baza _baza;
    private readonly IMapper _mapper;

    public GenreService(Baza baza, IMapper mapper)
    {
        _baza = baza;
        _mapper = mapper;
    }

    public ApiResponse<List<GetGenreResponse>> GetAllGenres()
    {
        var genres = _baza.Genres.OrderBy(g => g.Name).ToList();
        return ApiResponse<List<GetGenreResponse>>.OK(_mapper.Map<List<GetGenreResponse>>(genres));
    }

    public ApiResponse<CreateGenreResponse> CreateGenre (CreateGenreRequest request)
    {

        bool nameTaken = _baza.Genres.Any(g => g.Name.ToLower() == request.Name.ToLower());
        if (nameTaken)
        {
            return ApiResponse<CreateGenreResponse>.Fail(409, "Genre already exists.");
        }

        var genre = _mapper.Map<Genre>(request);

        _baza.Genres.Add(genre);
        _baza.SaveChanges();

        return ApiResponse<CreateGenreResponse>.OK(_mapper.Map<CreateGenreResponse>(genre));
    }
}
