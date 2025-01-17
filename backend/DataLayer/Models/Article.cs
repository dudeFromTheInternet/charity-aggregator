using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class Article
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ArticleId { get; set; }

    [Required]
    [StringLength(40)]
    public string Title { get; set; }
    
    public Charity Author { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public string Text { get; set; }
    
    public ArticlePhoto? Photo { get; set; }
}