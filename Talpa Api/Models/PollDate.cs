using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

public class PollDate
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	public required DateTime Date { get; init; }

	public required Poll Poll { get; init; }
}