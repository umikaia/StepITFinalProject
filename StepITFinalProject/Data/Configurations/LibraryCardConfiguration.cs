using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepITFinalProject.Models;

namespace StepITFinalProject.Data.Configurations;

public class LibraryCardConfiguration
{
    public void Configure(EntityTypeBuilder<LibraryCard> b)
    {
        b.HasKey(c => c.Id);
        b.Property(c => c.CardNumber).HasMaxLength(20).IsRequired();
        b.HasIndex(c => c.CardNumber).IsUnique();

        b.HasOne(c => c.User)
            .WithOne(u => u.LibraryCard)
            .HasForeignKey<LibraryCard>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasIndex(c => c.UserId).IsUnique(); // this is what makes it 1:1 in the database
    }
}
