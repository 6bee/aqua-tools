namespace aqua.tool.Validation.Tests;

public class AssertNotNullOrWhiteSpace
{
    [Fact]
    public void WithNull_ThrowsArgumentNullException()
    {
        string? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertNotNullOrWhiteSpace())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithWhitespace_ThrowsArgumentException()
    {
        const string value = "  ";

        Should.Throw<ArgumentException>(() => value.AssertNotNullOrWhiteSpace())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithEmpty_ThrowsArgumentException()
    {
        const string value = "";

        Should.Throw<ArgumentException>(() => value.AssertNotNullOrWhiteSpace())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithValue_DoesNotThrow()
    {
        const string value = "x";

        value.AssertNotNullOrWhiteSpace();
    }
}
