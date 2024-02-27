namespace Talpa_Api.Models;

public class Tag
{
    public required int Id { get; set; }
    
    public required string Title { get; set; }
    
    public required string Description { get; set; }
    
    public required bool Restrictive { get; set; }
}