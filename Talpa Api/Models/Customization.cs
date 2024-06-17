using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talpa_Api.Models;

[Table("Customization")]
public class Customization(string name, bool gradient, string color1, string? color2 = null, string? color3 = null, string? logoPath = null)
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; init; }

	public string Name { get; set; } = name;

	public string Color1 { get; set; } = color1;

	public string? Color2 { get; set; } = color2;

	public string? Color3 { get; set; } = color3;

	public bool Gradient { get; set; } = gradient;

	public string? LogoPath { get; set; } = logoPath;
}