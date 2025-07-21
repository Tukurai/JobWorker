using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Worker.Core.Models;

public class Queue
{
    [Required]
    public Guid Id { get; set; } // Unique identifier for the queue

    [Required]
    public string Name { get; set; } = null!; // Name of the queue

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // When the queue was created

    public List<QueueJob> Jobs { get; set; } = []; // List of jobs in this queue

    public int JobCount => Jobs.Count; // Total number of jobs in the queue
}
