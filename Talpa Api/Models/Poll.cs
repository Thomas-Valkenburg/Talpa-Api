using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public required DateTime EndDate { get; init; }

    public virtual required Team Team { get; init; }

    public virtual List<Suggestion> Suggestions { get; init; } = [];

    public virtual List<Vote> Votes { get; init; } = [];
}