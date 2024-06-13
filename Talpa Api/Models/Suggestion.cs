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

    public required User? Creator { get; init; }

    public List<Tag> Tags { get; init; } = [];
    
    public List<Poll> Polls { get; init; } = [];

    public List<Vote> Votes { get; init; } = [];
}