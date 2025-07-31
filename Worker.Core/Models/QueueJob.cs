using System.ComponentModel.DataAnnotations;

namespace Worker.Core.Models;

public class QueueJob : Job
{
    [Required]
    public JobStatus Status { get; set; } = JobStatus.Pending;

    [Required]
    public Guid QueueId { get; set; } // ID of the queue this job belongs to

    public DateTime? PickedUpAt { get; set; }  // when dequeued for processing  

    public DateTime? CompletedAt { get; set; } // when processing finished  

    public int AttemptCount { get; set; } = 0;

    public string? ErrorMessage { get; set; } // error info if failed  
}
