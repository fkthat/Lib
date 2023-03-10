using System.Runtime.InteropServices;

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

    /// <summary>
    /// Fills an array with random bytes.
    /// </summary>
    /// <param name="data">The array to fill.</param>
    /// <param name="offset">The index of the array to start the fill operation.</param>
    /// <param name="count">The number of bytes to fill.</param>
    void GetBytes(byte[] data, int offset, int count)
    {
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
        GetBytes(span);
    }

    /// <summary>
    /// Fills an array with random bytes.
    /// </summary>
    /// <param name="data">The array to fill.</param>
    void GetBytes(byte[] data)
    {
        _ = data ?? throw new ArgumentNullException(nameof(data));
        GetBytes(data, 0, data.Length);
    }

    /// <summary>
    /// Creates an array of bytes with a random sequence of values.
    /// </summary>
    /// <param name="count">The number of bytes of random values to create.</param>
    /// <returns></returns>
    byte[] GetBytes(int count)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count));
        }

        var data = new byte[count];
        GetBytes(data);
        return data;
    }

    /// <summary>
    /// Returns a non-negative random <c cref="int"/> less than the specified maximum.
    /// </summary>
    /// <param name="toExclusive">The exclusive upper bound of the random number returned.</param>
    int GetInt32(int toExclusive)
    {
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
            GetBytes(span);
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
    /// <param name="fromInclusive">The inclusive lower bound of the random number returned.</param>
    /// <param name="toExclusive">The exclusive upper bound of the random number returned.</param>
    int GetInt32(int fromInclusive, int toExclusive)
    {
        if (toExclusive <= fromInclusive)
        {
            throw new ArgumentOutOfRangeException(nameof(toExclusive));
        }

        return GetInt32(toExclusive - fromInclusive) + fromInclusive;
    }
}
