using DataLayer;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy",
        p =>
        {
            /*
            p.WithOrigins("http://localhost:63342");
            p.WithOrigins("http://localhost:63343");
            p.WithOrigins("http://localhost:49277");
            p.WithOrigins("http://localhost:65522");
            p.WithOrigins("http://localhost:63342");
            p.WithOrigins("http://127.0.0.1:8080");
            p.WithOrigins("http://localhost:8080");
            p.WithOrigins("http://158.160.82.113:8080");
            p.WithOrigins("http://172.23.0.3:80");
            */
            p.AllowAnyOrigin();
            //p.AllowCredentials();
            p.AllowAnyHeader();
            p.AllowAnyMethod();
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Charity Aggregator API", Version = "v1" });
});

var connectionString = builder.Configuration.GetConnectionString("Postgres");
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
app.UseSwagger().UseSwaggerUI();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CharityAggregatorContext>();
    context.Database.EnsureCreated();
}

app.MapControllers();
app.Run();