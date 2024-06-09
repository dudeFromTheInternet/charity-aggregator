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
    public DbSet<Article> Articles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Charity>().HasIndex(c => c.Username).IsUnique();
        
        modelBuilder.Entity<Charity>()
            .HasMany(c => c.CharityPhotos)
            .WithOne(p => p.Charity)
            .HasForeignKey(p => p.CharityId);
        
        modelBuilder.Entity<Charity>()
            .HasMany(c => c.CharityProjects)
            .WithOne(p => p.Charity)
            .HasForeignKey(p => p.CharityId);
        
        modelBuilder.Entity<CharityProject>()
            .HasMany(p => p.ProjectPhotos)
            .WithOne(ph => ph.CharityProject)
            .HasForeignKey(ph => ph.ProjectId);
        
        modelBuilder.Entity<CharityProject>()
            .HasMany(p => p.ProjectComments)
            .WithOne(c => c.CharityProject)
            .HasForeignKey(c => c.ProjectId);
        
        modelBuilder.Entity<ProjectCategoryMapping>()
            .HasKey(pc => new { pc.ProjectId, pc.CategoryId });

        modelBuilder.Entity<ProjectCategoryMapping>()
            .HasOne(pc => pc.CharityProject)
            .WithMany(p => p.ProjectCategoryMappings)
            .HasForeignKey(pc => pc.ProjectId);

        modelBuilder.Entity<ProjectCategoryMapping>()
            .HasOne(pc => pc.ProjectCategory)
            .WithMany(c => c.ProjectCategoryMappings)
            .HasForeignKey(pc => pc.CategoryId);

        modelBuilder.Entity<Article>()
            .HasIndex(a => a.ArticleId).IsUnique();
    }
    
    public CharityAggregatorContext(DbContextOptions<CharityAggregatorContext> options)
        : base(options)
    {
    }
}
