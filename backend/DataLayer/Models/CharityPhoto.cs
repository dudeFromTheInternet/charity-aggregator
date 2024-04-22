using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class CharityPhoto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PhotoId { get; set; }

    [ForeignKey("Charity")]
    public int CharityId { get; set; }
    public string PhotoUrl { get; set; }
    public string Description { get; set; }
    public Charity Charity { get; set; }
}