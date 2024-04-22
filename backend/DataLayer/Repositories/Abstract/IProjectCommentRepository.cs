using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface IProjectCommentRepository
{
    Task AddAsync(ProjectComment comment);
    Task UpdateAsync(ProjectComment comment);
    Task DeleteAsync(int id);
    Task<ProjectComment?> GetByIdAsync(int id);
    Task<IEnumerable<ProjectComment>> GetAllAsync();
    // Task<IEnumerable<ProjectComment>> GetAllInPeriodAsync(DateTime startDate, DateTime endDate);
}