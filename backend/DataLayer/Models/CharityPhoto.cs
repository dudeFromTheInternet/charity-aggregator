using System.ComponentModel.DataAnnotations;

namespace DataLayer.Models;

public class CharityPhoto
{
    public int Id { get; set; }
    public int CharityId { get; set; }
    
    public string PhotoUrl { get; set; }
    public string Description { get; set; }
}