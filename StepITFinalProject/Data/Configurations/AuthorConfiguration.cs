using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StepITFinalProject.Models;


namespace StepITFinalProject.Data.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> b)
    {
        b.HasKey(a => a.Id);
        b.Property(a => a.FirstName).HasMaxLength(60).IsRequired();
        b.Property(a => a.LastName).HasMaxLength(60).IsRequired();
        b.Property(a => a.Country).HasMaxLength(60).IsRequired();
    }
}
