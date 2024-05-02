using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy",
        p => p
            .WithOrigins("http://localhost:63345")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string connectionString = "Server=localhost;Database=charity_database;User Id=postgres;Password=postgres;";
builder.Services.AddDbContext<CharityAggregatorContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

app.UseCors("corsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CharityAggregatorContext>();
    context.Database.EnsureCreated();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CharityAggregatorContext>();
    
    if (!context.CharityProjects.Any())
    {
        var charity = new Charity
        {
            Name = "Charity 1",
            Description = "Description 1",
            ContactInfo = "Contact info 1",
            Username = "charity1",
            PasswordHash = "password1"
        };
        
        context.Charities.Add(charity);
        
        var project = new CharityProject
        {
            Name = "Project 1",
            Description = "Description 1",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(7),
            Charity = charity
        };
        
        context.CharityProjects.Add(project);
        
        var category = new ProjectCategory
        {
            Name = "Category 1"
        };
        
        context.ProjectCategories.Add(category);
        
        context.ProjectsCategoryMappings.Add(new ProjectCategoryMapping
        {
            CharityProject = project,
            ProjectCategory = category
        });
        
        context.ProjectPhotos.Add(new ProjectPhoto
        {
            CharityProject = project,
            Description = "Photo 1",
            PhotoBytes = "https://example.com/photo.jpg"
        });
        
        await context.SaveChangesAsync();
    }
}


app.MapGet("/CharityProjects/", async (CharityAggregatorContext context) =>
{
    var projects = await context.CharityProjects
        .Include(p => p.ProjectCategoryMappings)
        .ThenInclude(pc => pc.ProjectCategory)
        .Include(p => p.Charity)
        .Include(p => p.ProjectPhotos)
        .ToListAsync();
    
    var response = projects.Select(p => new
    {
        p.Name,
        Category = p.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
        p.Description,
        CharityName = p.Charity.Name,
        Photo = p.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
        p.StartDate,
        p.EndDate
    });
    
    return Results.Ok(response);
});

app.MapPost("/CharityProjects/", async (CharityAggregatorContext context, CharityProjectRequest request) =>
{
    var charity = await context.Charities
        .FirstOrDefaultAsync(c => c.Name == request.CharityName);
    
    if (charity == null)
    {
        charity = new Charity
        {
            Name = request.CharityName
        };
        
        context.Charities.Add(charity);
    }
    
    var project = new CharityProject
    {
        Name = request.Name,
        Description = request.Description,
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        Charity = charity
    };
    
    context.CharityProjects.Add(project);
    
    var categoryEntity = await context.ProjectCategories
        .FirstOrDefaultAsync(c => c.Name == request.Category);
    
    if (categoryEntity == null)
    {
        categoryEntity = new ProjectCategory
        {
            Name = request.Category
        };
        
        context.ProjectCategories.Add(categoryEntity);
    }
    
    context.ProjectsCategoryMappings.Add(new ProjectCategoryMapping
    {
        CharityProject = project,
        ProjectCategory = categoryEntity
    });
    
    context.ProjectPhotos.Add(new ProjectPhoto
    {
        CharityProject = project,
        Description = "Photo",
        PhotoBytes = request.Photo
    });
    
    await context.SaveChangesAsync();
    
    var response = new CharityProjectRequest
    {
        Name = project.Name,
        Description = project.Description,
        StartDate = project.StartDate,
        EndDate = project.EndDate,
        Category = request.Category,
        CharityName = charity.Name,
        Photo = request.Photo
    };
    
    return Results.Created($"/CharityProjects/{project.ProjectId}", response);
});

app.MapGet("/CharityProjects/{id:int}", async (CharityAggregatorContext context, int id) =>
{
    var project = await context.CharityProjects
        .Include(p => p.ProjectCategoryMappings)
        .ThenInclude(pc => pc.ProjectCategory)
        .Include(p => p.Charity)
        .Include(p => p.ProjectPhotos)
        .FirstOrDefaultAsync(p => p.ProjectId == id);
    
    if (project == null)
    {
        return Results.NotFound();
    }
    
    var response = new
    {
        project.Name,
        Category = project.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
        project.Description,
        CharityName = project.Charity.Name,
        Photo = project.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
        project.StartDate,
        project.EndDate
    };
    
    return Results.Ok(response);
});

app.Run();

public class CharityProjectRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string CharityName { get; set; }

    [Required]
    public string Photo { get; set; }

    public CharityProjectRequest() {}
}