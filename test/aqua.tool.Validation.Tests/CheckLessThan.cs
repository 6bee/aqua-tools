namespace aqua.tool.Validation.Tests;

public class CheckLessThan
{
    [Fact]
    public void WithNullableStructValue_ReturnsValue()
    {
        int? value = 0;

        value.CheckLessThan(1).ShouldBe(0);
    }

    [Fact]
    public void WithStructValue_ReturnsValue()
    {
        0.CheckLessThan(1).ShouldBe(0);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 1;

        Should.Throw<ArgumentOutOfRangeException>(() => value.CheckLessThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithGreaterInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 2;

        Should.Throw<ArgumentOutOfRangeException>(() => value.CheckLessThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckLessThan(1))
            .ParamName.ShouldBe("value");
    }
}
