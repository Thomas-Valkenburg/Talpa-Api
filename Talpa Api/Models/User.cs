using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("User")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(64)]
    public required string Id { get; init; }

    public int Points { get; set; } = 0;
    
    public required Team Team { get; init; }

    public List<Vote> Votes { get; init; } = [];

    public List<Suggestion> Suggestions { get; init; } = [];
}