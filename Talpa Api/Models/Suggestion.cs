using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Suggestion")]
public class Suggestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [StringLength(255)] 
    public required string Title { get; init; }

    [StringLength(1000)] 
    public required string Description { get; set; }

    [StringLength(255)] 
    public required string ImagePath { get; set; }

    public virtual required User? Creator { get; init; }

    public virtual List<Tag> Tags { get; init; } = [];
    
    public virtual List<Poll> Polls { get; init; } = [];

    public virtual List<Vote> Votes { get; init; } = [];
}

public readonly struct SuggestionWithSimilarity(int suggestionId, string title, double similarity)
{
    public int SuggestionId { get; init; } = suggestionId;
    public string Title { get; init; } = title;
    public double Similarity { get; init; } = similarity;
}