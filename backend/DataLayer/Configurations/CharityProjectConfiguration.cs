using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class CharityProjectConfiguration : IEntityTypeConfiguration<CharityProject>
{
    public void Configure(EntityTypeBuilder<CharityProject> builder)
    {
        throw new NotImplementedException();
    }
}