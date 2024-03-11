using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Team")]
public class Team
{
    [Key]
    public int Id { get; init; }

    [MaxLength(255)]
    public string Name { get; init; }
    
    public virtual List<User>? Users { get; init; }
}