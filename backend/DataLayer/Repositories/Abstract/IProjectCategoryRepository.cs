using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface IProjectCategoryRepository
{
    Task<ProjectCategory?> GetByIdAsync(int id);
    Task<IEnumerable<ProjectCategory>> GetAllAsync();
}