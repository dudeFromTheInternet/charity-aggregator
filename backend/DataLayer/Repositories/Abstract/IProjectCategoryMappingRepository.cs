using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface IProjectCategoryMappingRepository
{
    Task<IEnumerable<CharityProject>> GetAllByCategoryIdAsync(int categoryId);
    Task<IEnumerable<ProjectCategory>> GetAllByProjectIdAsync(int projectId);
}