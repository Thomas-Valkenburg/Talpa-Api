using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Suggestion")]
public class Suggestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [MaxLength(255)] public required string Title { get; init; }

    [MaxLength(1000)] public required string Description { get; set; }

    [MaxLength(255)] public string ImagePath { get; set; }

    public virtual required User? Creator { get; init; }

    public virtual List<Tag> Tags { get; init; } = [];
    
    public virtual List<Poll> Polls { get; init; } = [];

    public virtual List<Vote> Votes { get; init; } = [];
}