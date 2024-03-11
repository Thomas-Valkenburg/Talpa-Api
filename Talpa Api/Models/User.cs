using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("User")]
public class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)]
    public string Name { get; set; }

    public bool IsManager { get; set; }
    
    public virtual Team? Team { get; set; }
    
    public virtual List<Vote> Votes { get; set; } = [];
    
    public virtual List<Suggestion> Suggestions { get; set; } = [];
}