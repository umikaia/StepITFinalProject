using AutoMapper;
using StepITFinalProject.Common;
using StepITFinalProject.Data;
using StepITFinalProject.Models;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;
namespace StepITFinalProject.Services.Implementations;

public class LibraryCardService : ILibraryCardService
{
    private readonly Baza _baza;
    private readonly IMapper _mapper;

    public LibraryCardService(Baza baza, IMapper mapper)
    {
        _baza = baza;
        _mapper = mapper;
    }

    public ApiResponse<GetLibraryCardResponse> CreateMyCard(int userId)
    {
        bool hasCard = _baza.LibraryCards.Any(c => c.UserId == userId);
        if (hasCard)
        {
            return ApiResponse<GetLibraryCardResponse>.Fail(409, "You already have a library card");
        }

        int nextNumber = (_baza.LibraryCards.Max(c => (int?)c.Id) ?? 0) + 1;
        var now = DateTime.UtcNow;

        var card = new LibraryCard
        {
            CardNumber = $"LC-{nextNumber:D6}",
            IsuedAt = now,
            ExpirationDate = now.AddYears(1),
            UserId = userId
        };

        _baza.LibraryCards.Add(card);
        _baza.SaveChanges();

        return ApiResponse<GetLibraryCardResponse>.OK(_mapper.Map<GetLibraryCardResponse>(card), "Library card created successfully", 201);
    }

    public ApiResponse<GetLibraryCardResponse> GetMyCard(int userId)
    {
        var card = _baza.LibraryCards.FirstOrDefault(c => c.UserId == userId);
        if (card == null)
        {
            return ApiResponse<GetLibraryCardResponse>.Fail(404, "Library card not found");
        }

        return ApiResponse<GetLibraryCardResponse>.OK(_mapper.Map<GetLibraryCardResponse>(card));
    }
}
