namespace aqua.tool.Validation.Tests;

public class AssertNotNullOrEmpty
{
    [Fact]
    public void WithEnumerableUsesSlowPath()
    {
        IEnumerable empty = Iterator(hasItem: false);

        Should.Throw<ArgumentException>(() => empty.AssertNotNullOrEmpty())
            .ParamName.ShouldBe("empty");

        IEnumerable nonEmpty = Iterator(hasItem: true);
        nonEmpty.AssertNotNullOrEmpty();

        static IEnumerable Iterator(bool hasItem)
        {
            if (hasItem)
            {
                yield return 1;
            }
        }
    }

    [Fact]
    public void WithStringNull_ThrowsArgumentNullException()
    {
        string? value = null;

        Should.Throw<ArgumentNullException>(() => value.AssertNotNullOrEmpty())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithStringEmpty_ThrowsArgumentException()
    {
        const string value = "";

        Should.Throw<ArgumentException>(() => value.AssertNotNullOrEmpty())
            .ParamName.ShouldBe("value");
    }
}
