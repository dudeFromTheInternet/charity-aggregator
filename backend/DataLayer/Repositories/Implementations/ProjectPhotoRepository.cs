using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class ProjectPhotoRepository(CharityAggregatorContext context) : IProjectPhotoRepository
{
    public async Task AddAsync(ProjectPhoto charityPhoto)
    {
        await context.ProjectPhotos.AddAsync(charityPhoto);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProjectPhoto charityPhoto)
    {
        context.ProjectPhotos.Update(charityPhoto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var photo = await context.ProjectPhotos.FindAsync(id);
        if (photo != null)
        {
            context.ProjectPhotos.Remove(photo);
            await context.SaveChangesAsync();
        }
    }

    public async Task<ProjectPhoto?> GetByIdAsync(int id)
    {
        return await context.ProjectPhotos.FindAsync(id);
    }

    public async Task<IEnumerable<ProjectPhoto>> GetAllByProjectIdAsync(int projectId)
    {
        return await context.ProjectPhotos
            .Where(pp => pp.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProjectPhoto>> GetAllAsync()
    {
        return await context.ProjectPhotos.ToListAsync();
    }
}