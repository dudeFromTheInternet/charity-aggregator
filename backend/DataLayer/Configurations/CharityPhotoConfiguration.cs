using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class CharityPhotoConfiguration : IEntityTypeConfiguration<CharityPhoto>
{
    public void Configure(EntityTypeBuilder<CharityPhoto> builder)
    {
        throw new NotImplementedException();
    }
}