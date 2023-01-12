namespace FkThat.Mockables;

/// <summary>
/// System clock.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Gets a System.DateTimeOffset object whose date and time are set to the current Coordinated
    /// Universal Time (UTC) date and time and whose offset is System.TimeSpan.Zero.
    /// </summary>
    /// <value>
    /// An object whose date and time is the current Coordinated Universal Time (UTC) and whose
    /// offset is System.TimeSpan.Zero.
    /// </value>
    DateTimeOffset UtcNow { get; }
}
