using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Tag")]
public class Tag
{
    [Key]
    public int Id { get; init; }

    [MaxLength(255)]
    public string Title { get; init; }

    public bool Restrictive { get; init; }
    
    public virtual List<Suggestion>? Suggestions { get; init; }
}