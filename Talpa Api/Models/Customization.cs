namespace Talpa_Api.Models;

public class Customization(bool gradient, string color1, string? color2 = null, string? color3 = null, string? logoPath = null)
{
	public string Color1 { get; set; } = color1;

	public string? Color2 { get; set; } = color2;

	public string? Color3 { get; set; } = color3;

	public bool Gradient { get; set; } = gradient;

	public string? LogoPath { get; set; } = logoPath;
}