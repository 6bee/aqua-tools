namespace aqua.tool.Validation.Tests;

public class CheckNotNullOrWhiteSpace
{
    [Fact]
    public void WithNull_ThrowsArgumentNullException()
    {
        string? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckNotNullOrWhiteSpace())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithWhitespace_ThrowsArgumentException()
    {
        const string value = "  ";

        Should.Throw<ArgumentException>(() => value.CheckNotNullOrWhiteSpace())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithEmpty_ThrowsArgumentException()
    {
        const string value = "";

        Should.Throw<ArgumentException>(() => value.CheckNotNullOrWhiteSpace())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithValue_ReturnsValue()
    {
        const string value = "x";

        value.CheckNotNullOrWhiteSpace().ShouldBe(value);
    }
}
