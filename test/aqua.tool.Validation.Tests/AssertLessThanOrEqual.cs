namespace aqua.tool.Validation.Tests;

public class AssertLessThanOrEqual
{
    [Fact]
    public void WithNullableStructValue_DoesNotThrow()
    {
        int? value = 1;

        value.AssertLessThanOrEqual(1);
    }

    [Fact]
    public void WithStructValue_DoesNotThrow()
    {
        1.AssertLessThanOrEqual(1);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 2;

        Should.Throw<ArgumentOutOfRangeException>(() => value.AssertLessThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertLessThanOrEqual(1))
            .ParamName.ShouldBe("value");
    }
}
