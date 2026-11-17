using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StepITFinalProject.Common;
using StepITFinalProject.Data;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using StepITFinalProject.Services.Interfaces;

namespace StepITFinalProject.Services.Implementations;

public class BorrowService : IBorrowService
{
    private const int MaxActiveLoans = 3;
    private const int LoanDays = 14;

    private readonly Baza _baza;
    private readonly IMapper _mapper;
    public BorrowService(Baza baza, IMapper mapper)
    {
        _baza = baza;
        _mapper = mapper;
    }

    public ApiResponse<GetBorrowResponse> BorrowBook(int userId, BorrowRequest request)
    {
        var book = _baza.Books.FirstOrDefault(b => b.Id == request.BookId);
        if (book == null)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, "Book with this id does not exist");
        }


        var card = _baza.LibraryCards.FirstOrDefault(c => c.UserId == userId);
        if (card == null)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, "You need a library card to borrow a book");
        }
        if (card.ExpirationDate <= DateTime.UtcNow)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, "Your library card has expired");
        }


        bool hasThisBook = _baza.BorrowRecords.Any(r => r.UserId == userId && r.BookId == request.BookId && r.ReturnedAt == null);
        if (hasThisBook)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, "You have already borrowed this book");
        }

        int activeLoans = _baza.BorrowRecords.Count(r => r.UserId == userId && r.ReturnedAt == null);
        if (activeLoans >= MaxActiveLoans)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, $"You cannot have more than {MaxActiveLoans} active borrows");
        }

        if (book.AvailableCopies <= 0)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, "There are no available copies of this book");
        }

        var borrowTime = DateTime.UtcNow;
        var record = new BorrowRecord
        {
            UserId = userId,
            BookId = book.Id,
            BorrowedAt = borrowTime,
            DueDate = borrowTime.AddDays(LoanDays)
        };

        book.AvailableCopies -= 1;       
        _baza.BorrowRecords.Add(record);
        _baza.SaveChanges();

        return ApiResponse<GetBorrowResponse>.OK(LoadAndMap(record.Id), "Book borrowed successfully", 201);
    }

    public ApiResponse<GetBorrowResponse> ReturnBook(int userId, int borrowId)
    {
        var record = _baza.BorrowRecords
            .Include(r => r.Book)
            .FirstOrDefault(r => r.Id == borrowId);

        if (record == null)
        {
            return ApiResponse<GetBorrowResponse>.Fail(404, "Borrow record not found");
        }

        if (record.UserId != userId)
        {
            return ApiResponse<GetBorrowResponse>.Fail(403, "You can only return your own books");
        }

        if (record.ReturnedAt != null)
        {
            return ApiResponse<GetBorrowResponse>.Fail(400, "This book has already been returned");
        }

        record.ReturnedAt = DateTime.UtcNow;
        record.Book.AvailableCopies += 1;
        _baza.SaveChanges();

        return ApiResponse<GetBorrowResponse>.OK(LoadAndMap(record.Id), "Book returned successfully");
    }

    public ApiResponse<List<GetBorrowResponse>> GetMyBorrows(int userId, bool activeOnly)
    {
        var query = _baza.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.User)
            .Where(r => r.UserId == userId);

        if (activeOnly)
        {
            query = query.Where(r => r.ReturnedAt == null);
        }

        var records = query.OrderByDescending(r => r.BorrowedAt).ToList();
        return ApiResponse<List<GetBorrowResponse>>.OK(_mapper.Map<List<GetBorrowResponse>>(records));
    }

    public ApiResponse<List<GetBorrowResponse>> GetAllBorrows(bool overdueOnly, int? userId)
    {
        var query = _baza.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.User)
            .AsQueryable();

        if (userId.HasValue)
        {
            query = query.Where(r => r.UserId == userId.Value);
        }

        if (overdueOnly)
        {
            var currentTime = DateTime.UtcNow;  
            query = query.Where(r => r.ReturnedAt == null && r.DueDate < currentTime);
        }

        var records = query.OrderByDescending(r => r.BorrowedAt).ToList();

        return ApiResponse<List<GetBorrowResponse>>.OK(_mapper.Map<List<GetBorrowResponse>>(records));
    }

    private GetBorrowResponse LoadAndMap(int borrowId)
    {
        var record = _baza.BorrowRecords
            .Include(r => r.Book)
            .Include(r => r.User)
            .First(r => r.Id == borrowId);

        return _mapper.Map<GetBorrowResponse>(record);
    }

}
