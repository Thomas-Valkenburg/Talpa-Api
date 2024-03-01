using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
    [Key]
    public required int Id { get; init; }

    public required List<Suggestion>? Suggestions { get; init; }
}