using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Tag")]
public class Tag
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [MaxLength(255)]
    public required string Title { get; init; }

    public required bool Restrictive { get; init; }

    public List<Suggestion> Suggestions { get; init; } = [];
}