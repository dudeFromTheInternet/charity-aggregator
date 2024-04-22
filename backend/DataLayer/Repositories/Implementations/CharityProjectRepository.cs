using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DataLayer.Repositories.Implementations;

public class CharityProjectRepository(CharityAggregatorContext context): ICharityProjectRepository
{
    public async Task AddAsync(CharityProject charityProject)
    {
        await context.CharityProjects.AddAsync(charityProject);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CharityProject charityProject)
    {
        context.CharityProjects.Update(charityProject);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var charityProject = await context.CharityProjects.FindAsync(id);
        if (charityProject != null)
        {
            context.CharityProjects.Remove(charityProject);
            await context.SaveChangesAsync();
        }
    }

    public async Task<CharityProject?> GetByIdAsync(int id)
    {
        return await context.CharityProjects.FindAsync(id);
    }

    public async Task<IEnumerable<CharityProject>> GetAllAsync()
    {
        return await context.CharityProjects.ToListAsync();
    }
}