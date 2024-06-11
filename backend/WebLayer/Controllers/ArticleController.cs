using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebLayer.Models;

namespace WebLayer.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController(CharityAggregatorContext context) : Controller  
{
    [HttpGet]
    public async Task<IActionResult> GetArticles()
    {
        var articles = await context.Articles.ToListAsync();
        return Ok(articles);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetArticle(int id)
    {
        var article = await context.Articles.FindAsync(id);
        return Ok(article);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddArticle(ArticleRequest request)
    {
        var charity = await context.Charities
            .FirstOrDefaultAsync(c => c.Name == request.Author);

        if (charity == null)
        {
            charity = new Charity
            {
                Name = request.Author!
            };

            context.Charities.Add(charity);
        }

        var article = new Article
        {
            Title = request.Title,
            Author = charity,
            PublicationDate = request.PublicationDate.Value.ToUniversalTime(),
            Text = request.Text,
        };
        context.Articles.Add(article);
        context.ArticlePhotos.Add(new ArticlePhoto
        {
            Article = article,
            PhotoBytes = request.Photo!
        });

        await context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateArticle(Article article)
    {
        context.Articles.Update(article);
        await context.SaveChangesAsync();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        var article = await context.Articles.FindAsync(id);
        if (article != null)
        {
            context.Articles.Remove(article);
            await context.SaveChangesAsync();
        }
        return Ok();
    }
    [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredArticles([FromQuery] ArticleRequest filter)
        {
            IQueryable<Article> query = context.Articles
                .Include(p => p.Photo).Include(article => article.Author);

            if (!string.IsNullOrWhiteSpace(filter.Title))
                query = query.Where(p => p.Title.ToLower().Contains(filter.Title.ToLower()));
            
            if (!string.IsNullOrWhiteSpace(filter.Author))
                query = query.Where(p => p.Author.Name.ToLower().Contains(filter.Author.ToLower()));

            if(filter.StartFilterDate != null)
                query = query.Where(p => p.PublicationDate >= filter.StartFilterDate);
            if(filter.EndFilterDate != null)
                query = query.Where(p => p.PublicationDate <= filter.EndFilterDate);
            
            var articles = await query.ToListAsync();

            var response = articles.Select(p => new ArticleRequest
            {
                Title = p.Title,
                PublicationDate = p.PublicationDate,
                Photo = p.Photo?.PhotoBytes,
                Author = p.Author.Name,
                Text = p.Text,
            });

            return Ok(response);
        }
}