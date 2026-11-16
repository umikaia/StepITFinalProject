using StepITFinalProject.Services.Interfaces;
using StepITFinalProject.Common;
using StepITFinalProject.Data;
using StepITFinalProject.Models;
using StepITFinalProject.Requests;
using StepITFinalProject.Responses;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace StepITFinalProject.Services.Implementations;

public class BookService : IBookService
{
    private readonly Baza _baza;
    private readonly IMapper _mapper;

    public BookService(Baza baza, IMapper mapper)
    {
        _baza = baza;
        _mapper = mapper;
    }

    public ApiResponse<List<GetBookResponse>> GetAllBooks(string? search, int? authorId, int? genreId, bool onlyAvailable)
    {
        var querys = _baza.Books
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var text = search.Trim().ToLower();
            querys = querys.Where(b => b.Title.ToLower().Contains(text));
        }


        if (authorId.HasValue)
        {
            querys = querys.Where(b => b.AuthorId == authorId.Value);
        }

        if (genreId.HasValue)
        {
            querys = querys.Where(b => b.Genres.Any(g => g.Id == genreId.Value));
        }

        if (onlyAvailable)
        {
            querys = querys.Where(b => b.AvailableCopies > 0);
        }

        var books = querys.OrderBy(b => b.Title).ToList();

        return ApiResponse<List<GetBookResponse>>.OK(_mapper.Map<List<GetBookResponse>>(books));

    }

    public ApiResponse<GetBookDetailsResponse> GetBookById(int id)
    {
        var book = _baza.Books
            .Include(b => b.Author)
            .Include(b => b.Genres)
            .FirstOrDefault(b => b.Id == id);

        if (book == null)
        {
            return ApiResponse<GetBookDetailsResponse>.Fail(404, "Book not found");

        }

        return ApiResponse<GetBookDetailsResponse>.OK(_mapper.Map<GetBookDetailsResponse>(book));

    }

    public ApiResponse<CreateBookResponse> CreateBook(CreateBookRequest request)
    {
        bool authorExists = _baza.Authors.Any(a =>a.Id == request.AuthorId);

        if (!authorExists)
        {
            return ApiResponse<CreateBookResponse>.Fail(400, $"Author with id does not exist"); 

        }

        bool isbnTaken = _baza.Books.Any(b => b.ISBN == request.ISBN);

        if (isbnTaken)
        {
            return ApiResponse<CreateBookResponse>.Fail(409, "A book with this ISBN does not exist");
        }

        var genres = _baza.Genres.Where(g => request.GenreIds.Contains(g.Id)).ToList();
        if(genres.Count != request.GenreIds.Count)
        {
            return ApiResponse<CreateBookResponse>.Fail(400, "One or more genres do not exist");

        }

        var book = _mapper.Map<Book>(request);
        book.Genres = genres;

        _baza.Books.Add(book);
        _baza.SaveChanges();

        var response = _mapper.Map<CreateBookResponse>(book);

        return ApiResponse<CreateBookResponse>.OK(response, $"Book with Id {book.Id} created successfully");
    }

    public ApiResponse<GetBookDetailsResponse> UpdateBook(int id, UpdateBookRequest request)
    {
        var book = _baza.Books
            .Include(b => b.Genres)
            .FirstOrDefault(b => b.Id == id);

        if(book == null)
        {
            return ApiResponse<GetBookDetailsResponse>.Fail(404, "Book not found");
        }

        bool authorExists = _baza.Authors.Any(a => a.Id == request.AuthorId);

        if (!authorExists)
        {
            return ApiResponse<GetBookDetailsResponse>.Fail(400, "Authord does not exist");
        }

        bool isbnTaken = _baza.Books.Any(b => b.Id != id && b.ISBN == request.ISBN);

        if (isbnTaken)
        {
            return ApiResponse<GetBookDetailsResponse>.Fail(409, "A book with this ISBN already exists");
        }

        // aq dasamatebeli maqvs kidev borrowrecordebze
        //
        //

        var genres = _baza.Genres.Where(g => request.GenreIds.Contains(g.Id)).ToList();

        if(genres.Count != request.GenreIds.Count)
        {
            return ApiResponse<GetBookDetailsResponse>.Fail(400, "One or more genres do not exist");
        }

        _mapper.Map(request, book);

        book.Genres.Clear();
        book.Genres.AddRange(genres);

        _baza.SaveChanges();

        return ApiResponse<GetBookDetailsResponse>.OK(_mapper.Map<GetBookDetailsResponse>(book), "Book updatet successfully");

    }

    public ApiResponse<bool> DeleteBook(int id)
    {
        var book = _baza.Books.FirstOrDefault(b => b.Id == id);
        if(book == null)
        {
            return ApiResponse<bool>.Fail(404, "Book not found");
        }

        bool hasActiveBorrows = _baza.BorrowRecords.Any(r => r.BookId == id && r.ReturnedAt == null);
        if (hasActiveBorrows)
        {
            return ApiResponse<bool>.Fail(400, "Book has active borrows and cannot be deleted");
        }

        var history = _baza.BorrowRecords.Where(r => r.BookId == id).ToList();
        _baza.BorrowRecords.RemoveRange(history);

        _baza.Books.Remove(book);
        _baza.SaveChanges();

        return ApiResponse<bool>.OK(true, "Book deleted successfully", 204);
    }


}
