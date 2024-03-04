using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("User")]
public class User
{
    [Key]
    public required int Id { get; init; }

    [MaxLength(255)]
    public required string Name { get; init; }

    public required bool IsManager { get; init; }
    
    public required Team? Team { get; init; }
}