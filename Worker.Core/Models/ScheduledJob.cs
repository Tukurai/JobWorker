using System.ComponentModel.DataAnnotations;

namespace Worker.Core.Models;

public class ScheduledJob() : Job()
{
    [Required]
    public string CronExpression { get; set; } = null!;

    public DateTime? LastRunAt { get; set; }

    public DateTime? NextRunAt { get; set; }

    public bool IsEnabled { get; set; } = true;

    public DateTime? UpdatedAt { get; set; }
}
