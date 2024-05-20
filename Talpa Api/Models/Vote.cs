using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Vote")]
public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public required User Creator { get; init; }

    public required Suggestion Suggestion { get; set; }

    public required Poll Poll { get; init; }
}

public struct VotesWithCount
{
    public List<Vote> Votes { get; set; }
    public Dictionary<int, int> VotesPerSuggestion { get; set; }
}