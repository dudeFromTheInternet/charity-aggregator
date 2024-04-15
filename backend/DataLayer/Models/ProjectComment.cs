using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Models;

public class ProjectComment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CommentId { get; set; }

    [Required]
    public string CommentText { get; set; }

    [Required]
    public DateTime CommentDate { get; set; }

    [ForeignKey("CharityProject")]
    public int ProjectId { get; set; }
    public CharityProject CharityProject { get; set; }
}