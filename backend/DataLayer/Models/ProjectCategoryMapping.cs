using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class ProjectCategoryMapping
{
    [ForeignKey("CharityProject")]
    public int ProjectId { get; set; }

    [ForeignKey("ProjectCategory")]
    public int CategoryId { get; set; }

    public CharityProject CharityProject { get; set; }
    public ProjectCategory ProjectCategory { get; set; }

    [Key]
    public int ProjectCategoryMappingId { get; set; }
}