namespace aqua.tool.Validation.Tests;

public class CheckGreaterThan
{
    [Fact]
    public void WithNullableStructValue_ReturnsValue()
    {
        int? value = 2;

        value.CheckGreaterThan(1).ShouldBe(2);
    }

    [Fact]
    public void WithStructValue_ReturnsValue()
    {
        2.CheckGreaterThan(1).ShouldBe(2);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 1;

        Should.Throw<ArgumentOutOfRangeException>(() => value.CheckGreaterThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithSmallerInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 0;

        Should.Throw<ArgumentOutOfRangeException>(() => value.CheckGreaterThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckGreaterThan(1))
            .ParamName.ShouldBe("value");
    }
}
