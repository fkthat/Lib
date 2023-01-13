namespace FkThat.Mockables;

/// <summary>
/// System clock.
/// </summary>
public interface IClock
{
    /// <summary>
    /// Gets a <c cref="DateTimeOffset"/> object whose date and time are set to the current
    /// Coordinated Universal Time (UTC) date and time and whose offset is <c c="TimeSpan.Zero"/>.
    /// </summary>
    /// <value>
    /// An object whose date and time is the current Coordinated Universal Time (UTC) and whose
    /// offset is System.TimeSpan.Zero.
    /// </value>
    DateTimeOffset UtcNow { get; }

    /// <summary>
    /// Gets a <c cref="TimeZoneInfo"/> object that represents the local time zone.
    /// </summary>
    /// <value>An object that represents the local time zone.</value>
    TimeZoneInfo TimeZone { get; }

    /// <summary>
    /// Gets a <c cref="DateTimeOffset"/> object that is set to the current date and time on the
    /// current computer, with the offset set to the local time's offset from Coordinated Universal
    /// Time (UTC).
    /// </summary>
    /// <value>
    /// A <c cref="DateTimeOffset"/> object whose date and time is the current local time and whose
    /// offset is the local time zone's offset from Coordinated Universal Time (UTC).
    /// </value>
    DateTimeOffset Now
    {
        get
        {
            var utcNow = UtcNow;
            var offset = TimeZone.GetUtcOffset(utcNow);

            return new DateTimeOffset(
                utcNow.Ticks + offset.Ticks,
                TimeSpan.FromTicks(offset.Ticks));
        }
    }
}
