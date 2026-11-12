using Microsoft.EntityFrameworkCore;
using StepITFinalProject.Models;
namespace StepITFinalProject.Data;

public class Baza : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }
    public DbSet<LibraryCard> LibraryCards { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(
    DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryDatabase;Integrated Security=True;TrustServerCertificate=True;"
        );
    }

}
