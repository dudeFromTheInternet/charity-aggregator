using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations;

public class ProjectCategoryMappingConfiguration : IEntityTypeConfiguration<ProjectCategoryMapping>
{
    public void Configure(EntityTypeBuilder<ProjectCategoryMapping> builder)
    {
        throw new NotImplementedException();
    }
}