using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class ProjectPhoto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PhotoId { get; set; }

    [ForeignKey("CharityProject")]
    public int ProjectId { get; set; }
    public string PhotoBytes { get; set; }
    public string Description { get; set; }

    public CharityProject CharityProject { get; set; }
}