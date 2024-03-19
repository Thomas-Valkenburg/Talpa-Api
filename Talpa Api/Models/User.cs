using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("User")]
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [MaxLength(255)]
    public string Name { get; init; }

    public bool IsManager { get; init; }
    
    public virtual Team Team { get; init; }

    public virtual List<Vote> Votes { get; init; } = [];

    public virtual List<Suggestion> Suggestions { get; init; } = [];
}