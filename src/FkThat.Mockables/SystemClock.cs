namespace FkThat.Libs.Mockables;

/// <inheritdoc/>
public class SystemClock : IClock
{
    /// <inheritdoc/>
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
