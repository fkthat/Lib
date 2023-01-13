namespace FkThat.Mockables;

public class Test_IClock
{
    [Fact]
    public void Now_should_return_local_time()
    {
        var clock = A.Fake<FakeClock>();
        IClock sut = clock;

        var tz = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
        A.CallTo(() => clock.TimeZone).Returns(tz);

        // just before the summer time transition
        var utcNow = new DateTimeOffset(2023, 3, 26, 0, 0, 59, TimeSpan.Zero);
        A.CallTo(() => clock.UtcNow).Returns(utcNow);

        // should be +1h
        sut.Now.Should().Be(new DateTimeOffset(
            2023, 3, 26, 1, 0, 59, TimeSpan.FromHours(1)));

        // just after the summer time transition
        utcNow = new DateTimeOffset(2023, 3, 26, 1, 0, 1, TimeSpan.Zero);
        A.CallTo(() => clock.UtcNow).Returns(utcNow);

        // should be +2h
        sut.Now.Should().Be(new DateTimeOffset(
            2023, 3, 26, 3, 0, 1, TimeSpan.FromHours(2)));
    }
}

file abstract class FakeClock : IClock
{
    public abstract DateTimeOffset UtcNow { get; }

    public abstract TimeZoneInfo TimeZone { get; }
}
