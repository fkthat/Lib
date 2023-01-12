using System.Diagnostics.CodeAnalysis;

namespace FkThat.Mockables;

/// <summary>
/// Random number generator that uses <c cref="Random"/>.
/// </summary>
public class PseudoRandomGen : IRandomGen
{
    /// <inheritdoc/>
    [SuppressMessage("Security", "CA5394:Do not use insecure randomness")]
    [ExcludeFromCodeCoverage]
    public void GetBytes(Span<byte> data)
    {
        Random.Shared.NextBytes(data);
    }
}
