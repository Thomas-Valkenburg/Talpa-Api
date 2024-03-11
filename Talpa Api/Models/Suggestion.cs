using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Suggestion")]
public class Suggestion
{
    [Key]
    public int Id { get; set; }

    [MaxLength(255)] 
    public string Title { get; set; }

    public virtual User Creator { get; set; }

    public virtual List<Tag> Tags { get; set; } = [];

    public virtual List<Poll> Polls { get; set; } = [];

    public virtual List<Vote> Votes { get; set; } = [];
}