using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface ICharityProjectRepository
{
    Task AddAsync(CharityProject charityProject);
    Task UpdateAsync(CharityProject charityProject);
    Task DeleteAsync(int id);
    Task<CharityProject?> GetByIdAsync(int id);
    //Task<IEnumerable<CharityProject>> GetAllEndedAsync();
  //  Task<IEnumerable<CharityProject>> GetAllNotStartedAsync();
  //  Task<IEnumerable<CharityProject>> GetAllOngoingAsync();
  //  Task<IEnumerable<CharityProject>> GetAllByCharityIdAsync(int charityId);
   // Task<IEnumerable<CharityProject>> GetAllEndedByCharityIdAsync(int charityId);
    //Task<IEnumerable<CharityProject>> GetAllNotStartedByCharityIdAsync(int charityId);
    //Task<IEnumerable<CharityProject>> GetAllOngoingByCharityIdAsync(int charityId);
    Task<IEnumerable<CharityProject>> GetAllAsync();
}