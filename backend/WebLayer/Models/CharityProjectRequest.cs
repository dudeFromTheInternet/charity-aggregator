namespace WebLayer.Models;

public class CharityProjectRequest
{
    public int ID { get; set; }
    public string? Name { get; set; }
    
    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public IEnumerable<string>? Category { get; set; }

    public string? CharityName { get; set; }

    public string? Photo { get; set; }

    public CharityProjectRequest() {}
}