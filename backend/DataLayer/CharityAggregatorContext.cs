using System.Reflection;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer;

public class CharityAggregatorContext : DbContext
{
    public DbSet<Charity> Charities { get; set; }
    public DbSet<CharityPhoto> CharityPhotos { get; set; }
    public DbSet<CharityProject> CharityProjects { get; set; }
    public DbSet<ProjectCategory> ProjectCategories { get; set; }
    public DbSet<ProjectCategoryMapping> ProjectsCategoryMappings { get; set; }
    public DbSet<ProjectComment> ProjectComments { get; set; }
    public DbSet<ProjectPhoto> ProjectPhotos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public CharityAggregatorContext(DbContextOptions<CharityAggregatorContext> options)
        : base(options)
    {
        
    }
}
