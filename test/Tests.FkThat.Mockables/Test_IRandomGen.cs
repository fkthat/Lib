namespace FkThat.Mockables;

public class Test_IRandomGen
{
    // GetBytes(data, offset, count)

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_null_data()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();
        byte[] data = null!;

        FluentActions.Invoking(() => sut.GetBytes(data, 0, 4))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(data));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_offset()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();

        // negative offset
        int offset = -1;
        FluentActions.Invoking(() => sut.GetBytes(new byte[4], offset, 4))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(offset));

        // too large offset
        offset = 4;
        FluentActions.Invoking(() => sut.GetBytes(new byte[4], offset, 4))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(offset));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_check_count()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();

        // negative count
        int count = -1;
        FluentActions.Invoking(() => sut.GetBytes(new byte[4], 0, count))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(count));

        // too large count
        count = 5;
        FluentActions.Invoking(() => sut.GetBytes(new byte[4], 0, count))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(count));
    }

    [Fact]
    public void GetBytes_with_data_offset_count_should_fill_data()
    {
        var data = new byte[] { 16, 112, 193, 67 };
        var random = A.Fake<FakeRandomGen>();
        A.CallTo(() => random.GetFakeBytes(2)).Returns(new byte[] { 42, 73 });
        IRandomGen sut = random;
        sut.GetBytes(data, 1, 2);
        data.Should().Equal(new byte[] { 16, 42, 73, 67 });
    }

    // GetBytes(data)

    [Fact]
    public void GetBytes_with_data_should_check_null_data()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();
        byte[] data = null!;

        FluentActions.Invoking(() => sut.GetBytes(data))
            .Should().Throw<ArgumentNullException>().Which.ParamName
            .Should().Be(nameof(data));
    }

    [Fact]
    public void GetBytes_with_data_should_fill_data()
    {
        var data = new byte[] { 16, 112, 193, 67 };
        var random = A.Fake<FakeRandomGen>();
        A.CallTo(() => random.GetFakeBytes(4)).Returns(new byte[] { 235, 55, 221, 159 });
        IRandomGen sut = random;
        sut.GetBytes(data);
        data.Should().Equal(new byte[] { 235, 55, 221, 159 });
    }

    // GetBytes(count)

    [Fact]
    public void GetBytes_with_count_should_check_count()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();
        int count = -1;

        FluentActions.Invoking(() => sut.GetBytes(count))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(count));
    }

    [Fact]
    public void GetBytes_with_count_should_return_data()
    {
        var random = A.Fake<FakeRandomGen>();

        A.CallTo(() => random.GetFakeBytes(4))
            .Returns(new byte[] { 235, 55, 221, 159 });

        IRandomGen sut = random;
        var actual = sut.GetBytes(4);
        actual.Should().Equal(new byte[] { 235, 55, 221, 159 });
    }

    // GetInt32(toExclusive)

    [Fact]
    public void GetInt32_with_toExclusive_should_check_toExclusive()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();

        int toExclusive = 0;
        FluentActions.Invoking(() => sut.GetInt32(toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));

        toExclusive = -1;
        FluentActions.Invoking(() => sut.GetInt32(toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));
    }

    [Fact]
    public void GetInt32_with_toExclusive_should_return_random_int()
    {
        var random = A.Fake<FakeRandomGen>();

        A.CallTo(() => random.GetFakeBytes(4))
            .Returns(new byte[] { 0xff, 0xff, 0xff, 0xff }).Once().Then
            .Returns(new byte[] { 0x44, 0x8a, 0x6d, 0x13 }).Once().Then
            .Returns(new byte[] { 0x00, 0x00, 0x00, 0x00 });

        IRandomGen sut = random;
        var actual = sut.GetInt32(42);
        actual.Should().Be(4);
    }

    // GetInt32(fromInclusive, toExclusive)

    [Fact]
    public void GetInt32_with_fromInclusive_toExclusive_should_check_toExclusive()
    {
        IRandomGen sut = A.Fake<FakeRandomGen>();

        int toExclusive = 42;
        FluentActions.Invoking(() => sut.GetInt32(42, toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));

        toExclusive = 41;
        FluentActions.Invoking(() => sut.GetInt32(42, toExclusive))
            .Should().Throw<ArgumentOutOfRangeException>().Which.ParamName
            .Should().Be(nameof(toExclusive));
    }

    [Fact]
    public void GetInt32_with_fromInclusive_toExclusive_should_return_random_int()
    {
        var random = A.Fake<FakeRandomGen>();

        A.CallTo(() => random.GetFakeBytes(4))
            .Returns(new byte[] { 0xff, 0xff, 0xff, 0xff }).Once().Then
            .Returns(new byte[] { 0x44, 0x8a, 0x6d, 0x13 }).Once().Then
            .Returns(new byte[] { 0x00, 0x00, 0x00, 0x00 });

        IRandomGen sut = random;
        var actual = sut.GetInt32(42, 73);
        actual.Should().Be(46);
    }
}

// Fake helper
file abstract class FakeRandomGen : IRandomGen
{
    public void GetBytes(Span<byte> data) =>
        new Span<byte>(GetFakeBytes(data.Length)).CopyTo(data);

    public abstract byte[] GetFakeBytes(int count);
}
