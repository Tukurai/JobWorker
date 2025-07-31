namespace Worker.Core.Interfaces;

public interface IDbEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    Guid Id { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the date and time when the entity was last updated.
    /// </summary>
    DateTime UpdatedAt { get; set; }
}
