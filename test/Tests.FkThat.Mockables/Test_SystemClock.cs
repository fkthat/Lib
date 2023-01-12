namespace FkThat.Mockables;

public class Test_SystemClock
{
    [Fact]
    public async Task UtcNow_should_return_incremental_time()
    {
        SystemClock sut = new();

        List<DateTimeOffset> r = new();

        for (var i = 0; i < 16; i++)
        {
            r.Add(sut.UtcNow);
            await Task.Delay(1);
        }

        r.Should().BeInAscendingOrder();
    }

    [Fact]
    public void UtcNow_should_return_UTC_time()
    {
        SystemClock sut = new();
        var r = sut.UtcNow;
        r.Offset.Should().Be(TimeSpan.Zero);
    }
}
