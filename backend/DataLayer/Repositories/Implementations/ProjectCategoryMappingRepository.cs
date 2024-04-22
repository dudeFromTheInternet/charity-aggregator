using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class ProjectCategoryMappingRepository(CharityAggregatorContext context) : IProjectCategoryMappingRepository
{
    public async Task<IEnumerable<CharityProject>> GetAllByCategoryIdAsync(int categoryId)
    {
        return await context.ProjectsCategoryMappings
            .Where(pcm => pcm.CategoryId == categoryId)
            .Select(pcm => pcm.CharityProject)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProjectCategory>> GetAllByProjectIdAsync(int projectId)
    {
        return await context.ProjectsCategoryMappings
            .Where(pcm => pcm.ProjectId == projectId)
            .Select(pcm => pcm.ProjectCategory)
            .ToListAsync();
    }
}