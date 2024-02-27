namespace Talpa_Api.Models;

public class User
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required bool IsManager { get; set; } = false;
}