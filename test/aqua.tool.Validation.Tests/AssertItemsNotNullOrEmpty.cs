namespace aqua.tool.Validation.Tests;

public class AssertItemsNotNullOrEmpty
{
    [Fact]
    public void WithNullCollection_ThrowsArgumentNullException()
    {
        IEnumerable<string?>? collection = null;

        Should.Throw<ArgumentNullException>(() => collection.AssertItemsNotNullOrEmpty())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionContainingEmptyString_ThrowsArgumentException()
    {
        IEnumerable<string?> collection = ["x", ""];

        Should.Throw<ArgumentException>(() => collection.AssertItemsNotNullOrEmpty())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithValidCollection_DoesNotThrow()
    {
        IEnumerable<string?> collection = ["x", "y"];

        collection.AssertItemsNotNullOrEmpty();
    }
}
