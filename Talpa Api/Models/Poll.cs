using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
    [Key]
    public int Id { get; init; }
    
    public DateOnly EndDateTime { get; init; }
    
    public virtual Team Team { get; set; }

    public virtual List<Suggestion>? Suggestions { get; init; }
    
    public virtual List<Vote>? Votes { get; init; }
}