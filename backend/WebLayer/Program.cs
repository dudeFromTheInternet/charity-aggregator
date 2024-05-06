using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Charity Aggregator API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CharityAggregatorContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("corsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Charity Aggregator API v1");
    });
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

app.MapControllers();
app.Run();