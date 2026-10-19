namespace aqua.tool.Validation.Tests;

public class AssertLessThan
{
    [Fact]
    public void WithNullableStructValue_DoesNotThrow()
    {
        int? value = 0;

        value.AssertLessThan(1);
    }

    [Fact]
    public void WithStructValue_DoesNotThrow()
    {
        0.AssertLessThan(1);
    }

    [Fact]
    public void WithInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 1;

        Should.Throw<ArgumentOutOfRangeException>(() => value.AssertLessThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithGreaterInvalidValue_ThrowsArgumentOutOfRangeException()
    {
        int? value = 2;

        Should.Throw<ArgumentOutOfRangeException>(() => value.AssertLessThan(1))
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullValue_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertLessThan(1))
            .ParamName.ShouldBe("value");
    }
}
