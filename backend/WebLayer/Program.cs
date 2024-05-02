using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using WebLayer;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", 
        p => 
            p.WithOrigins("http://localhost:63343")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: {connectionString}");
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
    
    var response = projects.Select(p => new CharityProjectRequest
    {
        Name = p.Name,
        Category = p.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
        Description = p.Description,
        CharityName = p.Charity.Name,
        Photo = p.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
        StartDate = p.StartDate,
        EndDate = p.EndDate
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
    
    foreach (var categoryName in request.Category)
    {
        var category = await context.ProjectCategories
            .FirstOrDefaultAsync(c => c.Name == categoryName);
        
        if (category == null)
        {
            category = new ProjectCategory
            {
                Name = categoryName
            };
            
            context.ProjectCategories.Add(category);
        }
        
        context.ProjectsCategoryMappings.Add(new ProjectCategoryMapping
        {
            CharityProject = project,
            ProjectCategory = category
        });
    }
    
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
        return Results.NotFound("Project not found");
    }
    
    var response = new CharityProjectRequest
    {
        Name = project.Name,
        Category = project.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
        Description = project.Description,
        CharityName = project.Charity.Name,
        Photo = project.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
        StartDate = project.StartDate,
        EndDate = project.EndDate
    };
    
    return Results.Ok(response);
});

app.Run();