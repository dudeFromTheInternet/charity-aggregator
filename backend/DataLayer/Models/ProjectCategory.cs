using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class ProjectCategory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public IReadOnlyList<ProjectCategoryMapping> ProjectCategoryMappings { get; set; }
}