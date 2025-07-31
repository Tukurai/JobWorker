using System.ComponentModel.DataAnnotations;
using Worker.Core.Interfaces;

namespace Worker.Core.Models;

public class DbEntity : IDbEntity
{
    [Key]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
