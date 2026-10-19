namespace aqua.tool.Validation.Tests;

public class AssertNotNull
{
    [Fact]
    public void WithNullableStructNull_ThrowsArgumentNullException()
    {
        int? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertNotNull())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNullableStructValue_DoesNotThrow()
    {
        int? value = 42;

        value.AssertNotNull();
    }

    [Fact]
    public void WithClassNull_ThrowsArgumentNullException()
    {
        object? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertNotNull())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithClassValue_DoesNotThrow()
    {
        object value = new();

        value.AssertNotNull();
    }
}
