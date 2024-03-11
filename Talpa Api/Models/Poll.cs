using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
    [Key]
    public int Id { get; set; }
    
    public DateOnly EndDateTime { get; set; }
    
    public virtual Team Team { get; set; }

    public virtual List<Suggestion> Suggestions { get; set; } = [];

    public virtual List<Vote> Votes { get; set; } = [];
}