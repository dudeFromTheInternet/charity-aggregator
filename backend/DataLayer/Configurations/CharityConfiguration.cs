using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class CharityConfiguration : IEntityTypeConfiguration<Charity>
{
    public void Configure(EntityTypeBuilder<Charity> builder)
    {
        builder.HasIndex(c => c.UserName)
            .IsUnique();
    }
}