namespace aqua.tool.Validation.Tests;

public class CheckLessThanOrEqual
{
    [Fact]
    public void WithNullableStructValue_ReturnsValue()
    {
        int? value = 1;

        value.CheckLessThanOrEqual(1).ShouldBe(1);
    }

    [Fact]
    public void WithStructValue_ReturnsValue()
    {
        1.CheckLessThanOrEqual(1).ShouldBe(1);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 2;

        Should.Throw<ArgumentOutOfRangeException>(() => value.CheckLessThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckLessThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }
}
