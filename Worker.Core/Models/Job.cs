using System.ComponentModel.DataAnnotations;
using Worker.Core.Interfaces;

namespace Worker.Core.Models;

public class Job : DbEntity
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Payload { get; set; }  // JSON or serialized parameters  
}
