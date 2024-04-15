using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface ICharityProjectRepository
{
    public Task InsertAsync(CharityProject charityProject);
}