namespace Worker.Core.Models;

public enum JobType
{
    /// <summary>
    /// A job that send a notification.
    /// </summary>
    Notification,
    /// <summary>
    /// A job that approves all timesheets provided.
    /// </summary>
    ApproveAllTimesheets
}
