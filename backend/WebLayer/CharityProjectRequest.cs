using System.ComponentModel.DataAnnotations;

namespace WebLayer;

public class CharityProjectRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string CharityName { get; set; }

    [Required]
    public string Photo { get; set; }

    public CharityProjectRequest() {}
}