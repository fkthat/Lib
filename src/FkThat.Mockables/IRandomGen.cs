namespace FkThat.Mockables;

/// <summary>
/// Random number generator.
/// </summary>
public interface IRandomGen
{
    /// <summary>
    /// Fills a span with random bytes.
    /// </summary>
    /// <param name="data">The span to fill with random bytes.</param>
    void GetBytes(Span<byte> data);
}
