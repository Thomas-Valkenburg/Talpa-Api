using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Poll")]
public class Poll
{
	public Poll(DateTime endDate, List<DateTime> dates, Team team)
	{
        EndDate = endDate;
        Dates   = dates.Select(date => new PollDate {Date = date, Poll = this}).ToList();
        Team = team;
	}

	public Poll(DateTime endDate, List<DateTime> dates, List<Suggestion> suggestions, Team team)
	{
		EndDate     = endDate;
		Dates       = dates.Select(date => new PollDate { Date = date, Poll = this }).ToList();
		Suggestions = suggestions;
		Team        = team;
	}

	public Poll() { }

	[Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    public DateTime EndDate { get; init; }

    public List<PollDate> Dates { get; init; }

    public virtual Team Team { get; init; }

    public virtual List<Suggestion> Suggestions { get; init; } = [];

    public virtual List<Vote> Votes { get; init; } = [];
}