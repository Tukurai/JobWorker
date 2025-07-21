using System.ComponentModel.DataAnnotations;

namespace Worker.Core.Models;

public class Job()
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string? Payload { get; set; }  // JSON or serialized parameters  
}
