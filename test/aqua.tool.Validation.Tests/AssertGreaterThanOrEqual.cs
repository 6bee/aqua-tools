namespace aqua.tool.Validation.Tests;

public class AssertGreaterThanOrEqual
{
    [Fact]
    public void WithNullableStructValue_DoesNotThrow()
    {
        int? value = 2;

        value.AssertGreaterThanOrEqual(1);
    }

    [Fact]
    public void WithStructValue_DoesNotThrow()
    {
        2.AssertGreaterThanOrEqual(1);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 0;

        Should.Throw<ArgumentOutOfRangeException>(() => value.AssertGreaterThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertGreaterThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }
}
