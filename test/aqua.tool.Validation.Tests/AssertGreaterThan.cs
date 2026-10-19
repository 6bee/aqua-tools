namespace aqua.tool.Validation.Tests;

public class AssertGreaterThan
{
    [Fact]
    public void WithNullableStructValue_DoesNotThrow()
    {
        int? value = 2;

        value.AssertGreaterThan(1);
    }

    [Fact]
    public void WithStructValue_DoesNotThrow()
    {
        2.AssertGreaterThan(1);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 1;

        Should.Throw<ArgumentOutOfRangeException>(() => value.AssertGreaterThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithSmallerInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 0;

        Should.Throw<ArgumentOutOfRangeException>(() => value.AssertGreaterThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertGreaterThan(1))
            .ParamName.ShouldBe("value");
    }
}
