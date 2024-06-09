using DataLayer.Models;

namespace DataLayer.Repositories.Abstract;

public interface IArticleRepository
{
    Task AddAsync(Article article);
    Task UpdateAsync(Article article);
    Task DeleteAsync(int id);
    Task<Article?> GetByIdAsync(int id);
    Task<IEnumerable<Article>> GetAllAsync();
}