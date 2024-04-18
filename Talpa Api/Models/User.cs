using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("User")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(255)]
    public required string Id { get; init; }

    public int Points { get; set; } = 0;
    
    public virtual required Team Team { get; init; }

    public virtual List<Vote> Votes { get; init; } = [];

    public virtual List<Suggestion> Suggestions { get; init; } = [];
}