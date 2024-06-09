using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> AddArticle(Article article)
    {
        await context.Articles.AddAsync(article);
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
    
}