using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Vote")]
public class Vote
{
    [Key]
    public int Id { get; init; }

    public virtual User Creator { get; init; }
    
    public virtual Suggestion Suggestion { get; init; }
    
    public virtual Poll Poll { get; init; }
}