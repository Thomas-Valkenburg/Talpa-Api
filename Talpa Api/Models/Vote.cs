using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Vote")]
public class Vote
{
    [Key]
    public int Id { get; set; }

    public virtual User Creator { get; set; }
    
    public virtual Suggestion Suggestion { get; set; }
    
    public virtual Poll Poll { get; set; }
}