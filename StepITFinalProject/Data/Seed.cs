using StepITFinalProject.Enums;
using StepITFinalProject.Models;
namespace StepITFinalProject.Data.Seed;


public static class DbSeeder
{
    private const string LibrarianEmail = "librarian@library.com";
    private const string LibrarianPassword = "Librarian1";

    public static void Seed(Baza db)
    {

        var librarian = db.Users.FirstOrDefault(u => u.Email == LibrarianEmail);
        if (librarian == null)
        {
            db.Users.Add(new User
            {
                Email = LibrarianEmail,
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(LibrarianPassword),
                FullName = "Main Librarian",
                Role = ROLES.LIBRARIAN,
                CreatedAt = DateTime.UtcNow
            });
        }
        else if (librarian.Role != ROLES.LIBRARIAN)
        {
            librarian.Role = ROLES.LIBRARIAN;  
        }

        if (!db.Users.Any(u => u.Email == "reader@library.com"))
        {
            db.Users.Add(new User
            {
                Email = "reader@library.com",
                HashedPassword = BCrypt.Net.BCrypt.HashPassword("Reader123"),
                FullName = "Test Reader",
                Role = ROLES.READER,
                CreatedAt = DateTime.UtcNow
            });
        }

        // ---------- catalog (genres, authors, books) ----------
        bool catalogIsEmpty = !db.Genres.Any() && !db.Authors.Any() && !db.Books.Any();
        if (catalogIsEmpty)
        {
            var novel = new Genre { Name = "Novel" };
            var classic = new Genre { Name = "Classic" };
            var mystery = new Genre { Name = "Mystery" };
            var romance = new Genre { Name = "Romance" };

            var tolstoy = new Author { FirstName = "Leo", LastName = "Tolstoy", BirthYear = 1828, Country = "Russia" };
            var dostoevsky = new Author { FirstName = "Fyodor", LastName = "Dostoevsky", BirthYear = 1821, Country = "Russia" };
            var austen = new Author { FirstName = "Jane", LastName = "Austen", BirthYear = 1775, Country = "United Kingdom" };
            var christie = new Author { FirstName = "Agatha", LastName = "Christie", BirthYear = 1890, Country = "United Kingdom" };
            var chavchavadze = new Author { FirstName = "Ilia", LastName = "Chavchavadze", BirthYear = 1837, Country = "Georgia" };

            db.Books.AddRange(
                new Book { Title = "War and Peace", ISBN = "9780140447934", PublicationYear = 1869, TotalCopies = 3, AvailableCopies = 3, Author = tolstoy, Genres = new List<Genre> { novel, classic } },
                new Book { Title = "Crime and Punishment", ISBN = "9780140449136", PublicationYear = 1866, TotalCopies = 2, AvailableCopies = 2, Author = dostoevsky, Genres = new List<Genre> { novel, classic } },
                new Book { Title = "Pride and Prejudice", ISBN = "9780141439518", PublicationYear = 1813, TotalCopies = 4, AvailableCopies = 4, Author = austen, Genres = new List<Genre> { romance, classic } },
                new Book { Title = "Murder on the Orient Express", ISBN = "9780062693662", PublicationYear = 1934, TotalCopies = 2, AvailableCopies = 2, Author = christie, Genres = new List<Genre> { mystery } },
                new Book { Title = "Is the Man Human?", ISBN = "9789941000011", PublicationYear = 1863, TotalCopies = 1, AvailableCopies = 1, Author = chavchavadze, Genres = new List<Genre> { classic } });
        }

        db.SaveChanges();
    }
}
