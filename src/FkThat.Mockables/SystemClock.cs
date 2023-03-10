namespace FkThat.Mockables;

/// <inheritdoc/>
public class SystemClock : IClock
{
    /// <inheritdoc/>
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;

    /// <inheritdoc/>
    public TimeZoneInfo TimeZone => TimeZoneInfo.Local;
}
