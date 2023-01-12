using System.Runtime.InteropServices;

namespace FkThat.Libs.Mockables;

/// <summary>
/// Extension methods for <c cref="IRandomGen"/>.
/// </summary>
public static class RandomGenExtensions
{
    /// <summary>
    /// Fills an array with random bytes.
    /// </summary>
    /// <param name="random">The <c cref="IRandomGen"/> instanse.</param>
    /// <param name="data">The array to fill.</param>
    /// <param name="offset">The index of the array to start the fill operation.</param>
    /// <param name="count">The number of bytes to fill.</param>
    public static void GetBytes(this IRandomGen random, byte[] data, int offset, int count)
    {
        _ = random ?? throw new ArgumentNullException(nameof(random));
        _ = data ?? throw new ArgumentNullException(nameof(data));

        if (offset < 0 || offset > data.Length - 1)
        {
            throw new ArgumentOutOfRangeException(nameof(offset));
        }

        if (count < 0 || count > data.Length - offset)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        Span<byte> span = new(data, offset, count);
        random.GetBytes(span);
    }

    /// <summary>
    /// Fills an array with random bytes.
    /// </summary>
    /// <param name="random">The <c cref="IRandomGen"/> instanse.</param>
    /// <param name="data">The array to fill.</param>
    public static void GetBytes(this IRandomGen random, byte[] data)
    {
        _ = random ?? throw new ArgumentNullException(nameof(random));
        _ = data ?? throw new ArgumentNullException(nameof(data));
        GetBytes(random, data, 0, data.Length);
    }

    /// <summary>
    /// Creates an array of bytes with a random sequence of values.
    /// </summary>
    /// <param name="random">The <c cref="IRandomGen"/> instanse.</param>
    /// <param name="count">The number of bytes of random values to create.</param>
    /// <returns></returns>
    public static byte[] GetBytes(this IRandomGen random, int count)
    {
        _ = random ?? throw new ArgumentNullException(nameof(random));

        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var data = new byte[count];
        random.GetBytes(data);
        return data;
    }

    /// <summary>
    /// Returns a non-negative random <c cref="int"/> less than the specified maximum.
    /// </summary>
    /// <param name="random">The <c cref="IRandomGen"/> instanse.</param>
    /// <param name="toExclusive">The exclusive upper bound of the random number returned.</param>
    public static int GetInt32(this IRandomGen random, int toExclusive)
    {
        _ = random ?? throw new ArgumentNullException(nameof(random));

        if (toExclusive <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(toExclusive));
        }

        var mask = (uint)toExclusive - 1;
        mask |= mask >> 1;
        mask |= mask >> 2;
        mask |= mask >> 4;
        mask |= mask >> 8;
        mask |= mask >> 16;

        uint result = 0;
        Span<byte> span = MemoryMarshal.AsBytes(new Span<uint>(ref result));
        while (true)
        {
            random.GetBytes(span);
            result &= mask;

            if (result < toExclusive)
            {
                return (int)result;
            }
        }
    }

    /// <summary>
    /// Returns a random <c cref="int"/> within a specified range.
    /// </summary>
    /// <param name="random">The <c cref="IRandomGen"/> instanse.</param>
    /// <param name="fromInclusive">The inclusive lower bound of the random number returned.</param>
    /// <param name="toExclusive">The exclusive upper bound of the random number returned.</param>
    public static int GetInt32(this IRandomGen random, int fromInclusive, int toExclusive)
    {
        _ = random ?? throw new ArgumentNullException(nameof(random));

        if (toExclusive <= fromInclusive)
        {
            throw new ArgumentOutOfRangeException(nameof(toExclusive));
        }

        return random.GetInt32(toExclusive - fromInclusive) + fromInclusive;
    }
}
