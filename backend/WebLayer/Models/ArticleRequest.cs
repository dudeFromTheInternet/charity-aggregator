namespace WebLayer.Models;

public class ArticleRequest
{
    public int ArticleId { get; set; }
    
    public string Tittle { get; set; }
    
    public string Author { get; set; }
    
    public DateTime? PublicationDate { get; set; }
    public DateTime? StartFilterDate { get; set; }
    public DateTime? EndFilterDate { get; set; }
    public string Text { get; set; }
    
    public string? Photo { get; set; }
}