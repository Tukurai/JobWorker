namespace Worker.Core.Models;

public enum JobStatus
{
    /// <summary>
    /// The job is waiting to be processed.
    /// </summary>
    Pending,
    /// <summary>
    /// The job is currently being processed.
    /// </summary>
    Processing,
    /// <summary>
    /// The job has been successfully completed.
    /// </summary>
    Completed,
    /// <summary>
    /// The job has failed during processing.
    /// </summary>
    Failed,
    /// <summary>
    /// The job has been cancelled before processing.
    /// </summary>
    Cancelled
}
