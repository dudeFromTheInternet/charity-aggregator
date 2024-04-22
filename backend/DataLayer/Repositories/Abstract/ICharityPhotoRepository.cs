using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface ICharityPhotoRepository
{
    Task AddAsync(CharityPhoto charityPhoto);
    Task UpdateAsync(CharityPhoto charityPhoto);
    Task DeleteAsync(int id);
    Task<CharityPhoto?> GetByIdAsync(int id);
    Task<IEnumerable<CharityPhoto>> GetAllByCharityIdAsync(int charityId);
    Task<IEnumerable<CharityPhoto>> GetAllAsync();
}