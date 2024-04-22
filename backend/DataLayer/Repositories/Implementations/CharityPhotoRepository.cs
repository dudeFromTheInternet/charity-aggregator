using DataLayer.Models;
using DataLayer.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Implementations;

public class CharityPhotoRepository(CharityAggregatorContext context) : ICharityPhotoRepository
{
    public async Task AddAsync(CharityPhoto charityPhoto)
    {
        await context.CharityPhotos.AddAsync(charityPhoto);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CharityPhoto charityPhoto)
    {
        context.CharityPhotos.Update(charityPhoto);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var charityPhoto = await context.CharityPhotos.FindAsync(id);
        if (charityPhoto != null)
        {
            context.CharityPhotos.Remove(charityPhoto);
            await context.SaveChangesAsync();
        }
    }

    public async Task<CharityPhoto?> GetByIdAsync(int id)
    {
        return await context.CharityPhotos.FindAsync(id);
    }

    public async Task<IEnumerable<CharityPhoto>> GetAllByCharityIdAsync(int charityId)
    {
        return await context.CharityPhotos.Where(cp => cp.CharityId == charityId).ToListAsync();
    }

    public async Task<IEnumerable<CharityPhoto>> GetAllAsync()
    {
        return await context.CharityPhotos.ToListAsync();
    }
}