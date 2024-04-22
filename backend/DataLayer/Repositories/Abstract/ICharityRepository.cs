using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface ICharityRepository 
{
    Task AddAsync(Charity charity);
    Task UpdateAsync(Charity charity);
    Task DeleteAsync(int id);
    Task<Charity?> GetByIdAsync(int id);
    Task<IEnumerable<Charity>> GetAllAsync();
}
