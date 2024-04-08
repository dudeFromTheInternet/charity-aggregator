using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class Charity
{
    public int Id { get; set; }
    
    [Required] [MaxLength(100)]
    public string Name { get; set; }
    
    public string Description { get; set; }
    public string ContactInfo { get; set; }
    
    [Required] [MaxLength(50)]
    public string UserName { get; set; }
    
    [Required] [MaxLength(100)]
    public string PasswordHash { get; set; }
}