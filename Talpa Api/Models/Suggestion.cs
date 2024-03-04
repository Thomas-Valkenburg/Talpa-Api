using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Suggestion")]
public class Suggestion
{
    [Key]
    public required int Id { get; init; }

    public required User Creator { get; init; }

    [MaxLength(255)]
    public required string Title { get; init; }

    public required List<Tag>? Tags { get; init; }
    
    public required List<Poll>? Polls { get; init; }
}