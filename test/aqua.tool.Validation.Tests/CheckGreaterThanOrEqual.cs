namespace aqua.tool.Validation.Tests;

public class CheckGreaterThanOrEqual
{
    [Fact]
    public void WithNullableStructValue_ReturnsValue()
    {
        int? value = 2;

        value.CheckGreaterThanOrEqual(1).ShouldBe(2);
    }

    [Fact]
    public void WithStructValue_ReturnsValue()
    {
        2.CheckGreaterThanOrEqual(1).ShouldBe(2);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 0;

        Should.Throw<ArgumentOutOfRangeException>(() => value.CheckGreaterThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckGreaterThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }
}
