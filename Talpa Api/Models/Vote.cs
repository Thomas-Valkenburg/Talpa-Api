using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Vote")]
public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public virtual required User CreatorId { get; init; }

    public virtual required Suggestion Suggestion { get; init; }

    public virtual required Poll Poll { get; init; }
}