using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Tag")]
public class Tag
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string Title { get; set; }

    public bool Restrictive { get; set; }
    
    public virtual List<Suggestion>? Suggestions { get; set; }
}