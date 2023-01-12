namespace FkThat.Libs.Mockables;

public class Test_SystemGuidGen
{
    [Fact]
    public void NewGuid_should_return_unique_values()
    {
        SystemGuidGen sut = new();
        var r = Enumerable.Repeat(0, 42).Select(_ => sut.NewGuid());
        r.Should().OnlyHaveUniqueItems();
    }
}
