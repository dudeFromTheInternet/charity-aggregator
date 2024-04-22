using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class ProjectCommentRepository(CharityAggregatorContext context) : IProjectCommentRepository
{
    public async Task AddAsync(ProjectComment comment)
    {
        await context.ProjectComments.AddAsync(comment);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProjectComment comment)
    {
        context.ProjectComments.Update(comment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await context.ProjectComments.FindAsync(id);
        if (comment != null)
        {
            context.ProjectComments.Remove(comment);
            await context.SaveChangesAsync();
        }
    }

    public async Task<ProjectComment?> GetByIdAsync(int id)
    {
        return await context.ProjectComments.FindAsync(id);
    }

    public async Task<IEnumerable<ProjectComment>> GetAllAsync()
    {
        return await context.ProjectComments.ToListAsync();
    }

    // public async Task<IEnumerable<ProjectComment>> GetAllInPeriodAsync(DateTime startDate, DateTime endDate)
    // {
    //     throw new NotImplementedException();
    // }
}