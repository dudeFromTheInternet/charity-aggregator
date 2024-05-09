using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLayer.Models;

namespace WebLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharityProjectsController(CharityAggregatorContext context) : Controller   
    {
        [HttpGet]
        public async Task<IActionResult> GetCharityProjects()
        {
            var projects = await context.CharityProjects
                .Include(p => p.ProjectCategoryMappings)
                .ThenInclude(pc => pc.ProjectCategory)
                .Include(p => p.Charity)
                .Include(p => p.ProjectPhotos)
                .ToListAsync();

            var response = projects.Select(p => new CharityProjectRequest
            {
                ID = p.ProjectId,
                Name = p.Name,
                Category = p.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
                Description = p.Description,
                CharityName = p.Charity.Name,
                Photo = p.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            });

            return Ok(response);
        }
        [HttpGet("allCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await context
                .ProjectCategories
                .Select(c => c.Name)
                .Distinct()
                .ToArrayAsync();

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> PostCharityProject(CharityProjectRequest request)
        {
            var charity = await context.Charities
                .FirstOrDefaultAsync(c => c.Name == request.CharityName);

            if (charity == null)
            {
                charity = new Charity
                {
                    Name = request.CharityName!
                };

                context.Charities.Add(charity);
            }
            
            if (request.StartDate != null)
                request.StartDate = request.StartDate.Value.ToUniversalTime();
            if (request.EndDate != null)
                request.EndDate = request.EndDate.Value.ToUniversalTime();

            var project = new CharityProject
            {
                Name = request.Name!,
                Description = request.Description!,
                StartDate = request.StartDate ?? DateTime.MinValue,
                EndDate = request.EndDate ?? DateTime.MaxValue,
                Charity = charity
            };

            context.CharityProjects.Add(project);

            foreach (var categoryName in request.Category!)
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
                PhotoBytes = request.Photo!
            });

            await context.SaveChangesAsync();

            var response = new CharityProjectRequest
            {
                ID = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Category = request.Category,
                CharityName = charity.Name,
                Photo = request.Photo
            };

            return Created($"/CharityProjects/{project.ProjectId}", response);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCharityProject(int id)
        {
            var project = await context.CharityProjects
                .Include(p => p.ProjectCategoryMappings)
                .ThenInclude(pc => pc.ProjectCategory)
                .Include(p => p.Charity)
                .Include(p => p.ProjectPhotos)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound("Project not found");
            }

            var response = new CharityProjectRequest
            {
                ID = project.ProjectId,
                Name = project.Name,
                Category = project.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
                Description = project.Description,
                CharityName = project.Charity.Name,
                Photo = project.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
                StartDate = project.StartDate,
                EndDate = project.EndDate
            };

            return Ok(response);
        }
        
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredCharityProjects([FromQuery] CharityProjectRequest filter)
        {
            IQueryable<CharityProject> query = context.CharityProjects
                .Include(p => p.ProjectCategoryMappings)
                .ThenInclude(pc => pc.ProjectCategory)
                .Include(p => p.Charity)
                .Include(p => p.ProjectPhotos);

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Description))
                query = query.Where(p => p.Description.ToLower().Contains(filter.Description.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.CharityName))
                query = query.Where(p => p.Charity.Name.ToLower().Contains(filter.CharityName.ToLower()));

            if (filter.StartDate != null)
                query = query.Where(p => p.StartDate >= filter.StartDate);

            if (filter.EndDate != null)
                query = query.Where(p => p.EndDate <= filter.EndDate);
            filter.Category = filter.Category?.ToArray()[0].Split(',');
            if (filter.Category != null && filter.Category.Any(a => !string.IsNullOrWhiteSpace(a)))
            {
                query = query.Where(p =>
                    p.ProjectCategoryMappings.Any(pc => filter.Category.Contains(pc.ProjectCategory.Name)));
            }

            var projects = await query.ToListAsync();

            var response = projects.Select(p => new CharityProjectRequest
            {
                ID = p.ProjectId,
                Name = p.Name,
                Category = p.ProjectCategoryMappings.Select(pc => pc.ProjectCategory.Name),
                Description = p.Description,
                CharityName = p.Charity.Name,
                Photo = p.ProjectPhotos.FirstOrDefault()?.PhotoBytes,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            });

            return Ok(response);
        }
    }
}
