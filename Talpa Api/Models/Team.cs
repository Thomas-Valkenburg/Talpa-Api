using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Team")]
public class Team
{
    [Key]
    public required int Id { get; init; }

    [MaxLength(255)]
    public required string Name { get; init; }
    
    public required List<User>? Members { get; init; }
}