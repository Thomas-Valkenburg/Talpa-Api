namespace Talpa_Api.Models;

public class Suggestion
{
    public required int Id { get; set; }
    
    public required int CreatorId { get; set; }
    
    public required string Name { get; set; }
}