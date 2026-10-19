namespace aqua.tool.Validation.Tests;

public class CheckNotNull
{
    [Fact]
    public void WithNullableStructNull_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckNotNull())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullableStructValue_ReturnsValue()
    {
        int? value = 42;

        value.CheckNotNull().ShouldBe(42);
    }

    [Fact]
    public void WithNull_ThrowsArgumentNullException()
    {
        object? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckNotNull())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithValue_ReturnsSameInstance()
    {
        var value = new object();

        value.CheckNotNull().ShouldBeSameAs(value);
    }
}
