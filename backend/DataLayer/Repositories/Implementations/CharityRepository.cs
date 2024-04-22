using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class CharityRepository(CharityAggregatorContext context) : ICharityRepository
{
    public async Task AddAsync(Charity charity)
    {
        await context.Charities.AddAsync(charity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Charity charity)
    {
        context.Charities.Update(charity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var charity = await context.Charities.FindAsync(id);
        if (charity != null)
        {
            context.Charities.Remove(charity);
            await context.SaveChangesAsync();
        }
    }

    public async Task<Charity?> GetByIdAsync(int id)
    {
        return await context.Charities.FindAsync(id);
    }

    public async Task<IEnumerable<Charity>> GetAllAsync()
    {
        return await context.Charities.ToListAsync();
    }
}