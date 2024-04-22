using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class ProjectCategoryRepository(CharityAggregatorContext context) : IProjectCategoryRepository
{
    public async Task<ProjectCategory?> GetByIdAsync(int id)
    {
        return await context.ProjectCategories.FindAsync(id);
    }

    public async Task<IEnumerable<ProjectCategory>> GetAllAsync()
    {
        return await context.ProjectCategories.ToListAsync();
    }
}