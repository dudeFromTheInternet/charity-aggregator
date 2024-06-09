using DataLayer.Models;
using DataLayer.Repositories.Abstract;

namespace DataLayer.Repositories.Implementations;

public class ArticleRepository(CharityAggregatorContext context) : IArticleRepository
{
    public Task AddAsync(Article article)
    {
        context.Articles.Add(article);
        return context.SaveChangesAsync();
    }

    public Task UpdateAsync(Article article)
    {
        context.Articles.Update(article);
        return context.SaveChangesAsync();
    }

    public Task DeleteAsync(int id)
    {
        var article = context.Articles.Find(id);
        if (article == null) return Task.CompletedTask;
        context.Articles.Remove(article);
        return context.SaveChangesAsync();
    }

    public Task<Article?> GetByIdAsync(int id)
    {
        var article = context.Articles.Find(id);
        return Task.FromResult(article);
    }

    public Task<IEnumerable<Article>> GetAllAsync()
    {
        return Task.FromResult(context.Articles.AsEnumerable());
    }
}