using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class ArticlePhoto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ArticlePhotoId { get; set; }
    [ForeignKey("Article")]
    public int ArticleId { get; set; }
    public string PhotoBytes { get; set; }
    public Article Article { get; set; }
    
}