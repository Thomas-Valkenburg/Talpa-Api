using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public DateOnly EndDateTime { get; init; }

    public virtual required Team Team { get; init; }

    public virtual List<Suggestion> Suggestions { get; init; } = [];

    public virtual List<Vote> Votes { get; init; } = [];
}