using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class Article
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ArticleId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public string? Text { get; set; }
}