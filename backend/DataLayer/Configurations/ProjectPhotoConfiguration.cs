using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class ProjectPhotoConfiguration : IEntityTypeConfiguration<ProjectPhoto>
{
    public void Configure(EntityTypeBuilder<ProjectPhoto> builder)
    {
        throw new NotImplementedException();
    }
}