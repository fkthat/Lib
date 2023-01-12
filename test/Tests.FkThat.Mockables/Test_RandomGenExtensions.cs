namespace FkThat.Mockables;

public class Test_RandomGenExtensions
{
    // GetBytes(data, offset, count)

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_null_random()
    {
        IRandomGen random = null!;

        FluentActions.Invoking(() => random.GetBytes(new byte[4], 0, 4))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(random));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_null_data()
    {
        IRandomGen random = A.Fake<IRandomGen>();
        byte[] data = null!;
        FluentActions.Invoking(() => random.GetBytes(data, 0, 4))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(data));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_offset()
    {
        IRandomGen random = A.Fake<IRandomGen>();
        int offset;

        // negative offset
        offset = -1;
        FluentActions.Invoking(() => random.GetBytes(new byte[4], offset, 4))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(offset));

        // too large offset
        offset = 4;
        FluentActions.Invoking(() => random.GetBytes(new byte[4], offset, 4))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(offset));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_count()
    {
        IRandomGen random = A.Fake<IRandomGen>();
        int count;

        // negative count
        count = -1;
        FluentActions.Invoking(() => random.GetBytes(new byte[4], 0, count))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(count));

        // too large count
        count = 5;
        FluentActions.Invoking(() => random.GetBytes(new byte[4], 0, count))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(count));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_fill_data()
    {
        var data = new byte[] { 16, 112, 193, 67 };
        var random = A.Fake<FakeRandomGen>();
        A.CallTo(() => random.GetBytes(2)).Returns(new byte[] { 42, 73 });
        RandomGenExtensions.GetBytes(random, data, 1, 2);
        data.Should().Equal(new byte[] { 16, 42, 73, 67 });
    }

    // GetBytes(data)

    [Fact]
    public void GetBytes_with_data_should_check_null_random()
    {
        IRandomGen random = null!;

        FluentActions.Invoking(() => RandomGenExtensions.GetBytes(random, new byte[4]))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(random));
    }

    [Fact]
    public void GetBytes_with_data_should_check_null_data()
    {
        IRandomGen random = A.Fake<IRandomGen>();
        byte[] data = null!;

        FluentActions.Invoking(() => RandomGenExtensions.GetBytes(random, data))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(data));
    }

    [Fact]
    public void GetBytes_with_data_should_fill_data()
    {
        var data = new byte[] { 16, 112, 193, 67 };
        var random = A.Fake<FakeRandomGen>();
        A.CallTo(() => random.GetBytes(4)).Returns(new byte[] { 235, 55, 221, 159 });
        RandomGenExtensions.GetBytes(random, data);
        data.Should().Equal(new byte[] { 235, 55, 221, 159 });
    }

    // GetBytes(count)

    [Fact]
    public void GetBytes_with_count_should_check_null_random()
    {
        IRandomGen random = null!;

        FluentActions.Invoking(() => random.GetBytes(4))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(random));
    }

    [Fact]
    public void GetBytes_with_count_should_check_count()
    {
        var random = A.Fake<IRandomGen>();
        int count = -1;
        FluentActions.Invoking(() => random.GetBytes(count))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(count));
    }

    [Fact]
    public void GetBytes_with_count_should_return_data()
    {
        var random = A.Fake<FakeRandomGen>();
        A.CallTo(() => random.GetBytes(4)).Returns(new byte[] { 235, 55, 221, 159 });
        var actual = RandomGenExtensions.GetBytes(random, 4);
        actual.Should().Equal(new byte[] { 235, 55, 221, 159 });
    }

    // GetInt32(toExclusive)

    [Fact]
    public void GetInt32_with_toExclusive_should_check_null_random()
    {
        IRandomGen random = null!;

        FluentActions.Invoking(() => random.GetInt32(42))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(random));
    }

    [Fact]
    public void GetInt32_with_toExclusive_should_check_toExclusive()
    {
        IRandomGen random = A.Fake<IRandomGen>();

        int toExclusive;

        toExclusive = 0;
        FluentActions.Invoking(() => random.GetInt32(toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));

        toExclusive = -1;
        FluentActions.Invoking(() => random.GetInt32(toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));
    }

    [Fact]
    public void GetInt32_with_toExclusive_should_return_random_int()
    {
        var random = A.Fake<FakeRandomGen>();

        A.CallTo(() => random.GetBytes(4))
            .Returns(new byte[] { 0xff, 0xff, 0xff, 0xff }).Once().Then
            .Returns(new byte[] { 0x44, 0x8a, 0x6d, 0x13 }).Once().Then
            .Returns(new byte[] { 0x00, 0x00, 0x00, 0x00 });

        var actual = random.GetInt32(42);
        actual.Should().Be(4);
    }

    // GetInt32(fromInclusive, toExclusive)

    [Fact]
    public void GetInt32_with_fromInclusive_toExclusive_should_check_null_random()
    {
        IRandomGen random = null!;

        FluentActions.Invoking(() => random.GetInt32(42, 73))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(random));
    }

    [Fact]
    public void GetInt32_with_fromInclusive_toExclusive_should_check_toExclusive()
    {
        IRandomGen random = A.Fake<IRandomGen>();

        int toExclusive;

        toExclusive = 42;
        FluentActions.Invoking(() => random.GetInt32(42, toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));

        toExclusive = 41;
        FluentActions.Invoking(() => random.GetInt32(42, toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));
    }

    [Fact]
    public void GetInt32_with_fromInclusive_toExclusive_should_return_random_int()
    {
        var random = A.Fake<FakeRandomGen>();

        A.CallTo(() => random.GetBytes(4))
            .Returns(new byte[] { 0xff, 0xff, 0xff, 0xff }).Once().Then
            .Returns(new byte[] { 0x44, 0x8a, 0x6d, 0x13 }).Once().Then
            .Returns(new byte[] { 0x00, 0x00, 0x00, 0x00 });

        var actual = random.GetInt32(42, 73);
        actual.Should().Be(46);
    }

    // Fake helper

    internal abstract class FakeRandomGen : IRandomGen
    {
        public void GetBytes(Span<byte> data) =>
            new Span<byte>(GetBytes(data.Length)).CopyTo(data);

        public abstract byte[] GetBytes(int count);
    }
}
