using System.ComponentModel.DataAnnotations;
using Worker.Core.Interfaces;

namespace Worker.Core.Models;

public class Queue : DbEntity
{
    [Required]
    public string Name { get; set; } = null!; // Name of the queue

    public List<QueueJob> Jobs { get; set; } = []; // List of jobs in this queue

    public int JobCount => Jobs.Count; // Total number of jobs in the queue
}
