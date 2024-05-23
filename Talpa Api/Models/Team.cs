using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Team")]
public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(64)]
    public required string Id { get; init; }

    public Poll? Poll { get; set; }

    public List<User> Users { get; init; } = [];

}