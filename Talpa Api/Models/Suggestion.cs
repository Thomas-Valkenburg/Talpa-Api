using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Suggestion")]
public class Suggestion
{
    [Key]
    public int Id { get; init; }

    public virtual User Creator { get; init; }

    [MaxLength(255)]
    public string Title { get; init; }

    public virtual List<Tag>? Tags { get; init; }
    
    public virtual List<Poll>? Polls { get; init; }
    
    public virtual List<Vote>? Votes { get; init; }
}