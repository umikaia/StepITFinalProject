using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepITFinalProject.Models;

namespace StepITFinalProject.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> b)
    {
        b.HasKey(x => x.Id);
        b.Property(x => x.Title).HasMaxLength(200).IsRequired();
        b.Property(x => x.ISBN).HasMaxLength(13).IsRequired();
        b.HasIndex(x => x.ISBN).IsUnique();

        // the database refuses impossible stock numbers
        b.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Books_TotalCopies", "[TotalCopies] >= 1 AND [TotalCopies] <= 1000");
            t.HasCheckConstraint("CK_Books_AvailableCopies", "[AvailableCopies] >= 0 AND [AvailableCopies] <= [TotalCopies]");
        });

        b.HasOne(x => x.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict); // an author who has books can't be deleted

        b.HasMany(x => x.Genres)
            .WithMany(g => g.Books)
            .UsingEntity(j => j.ToTable("BookGenres"));
    }
}