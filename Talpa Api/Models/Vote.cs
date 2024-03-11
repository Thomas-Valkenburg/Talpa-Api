using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Vote")]
public class Vote
{
    [Key]
    public required int Id { get; init; }

    public required User Creator { get; init; }
    
    public required Suggestion Suggestion { get; init; }
    
    public required Poll Poll { get; init; }
}