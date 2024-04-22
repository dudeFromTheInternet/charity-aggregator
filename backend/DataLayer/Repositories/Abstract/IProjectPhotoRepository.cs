using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface IProjectPhotoRepository
{
    Task AddAsync(ProjectPhoto charityPhoto);
    Task UpdateAsync(ProjectPhoto charityPhoto);
    Task DeleteAsync(int id);
    Task<ProjectPhoto?> GetByIdAsync(int id);
    Task<IEnumerable<ProjectPhoto>> GetAllByProjectIdAsync(int projectId);
    Task<IEnumerable<ProjectPhoto>> GetAllAsync();
}