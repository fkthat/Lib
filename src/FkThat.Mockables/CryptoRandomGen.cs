using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace FkThat.Libs.Mockables;

/// <summary>
/// Secure random number generator.
/// </summary>
public sealed class CryptoRandomGen : IRandomGen
{
    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public void GetBytes(Span<byte> data)
    {
        RandomNumberGenerator.Fill(data);
    }
}
