namespace aqua.tool.Validation.Tests;

public class AssertItemsNotNullOrWhiteSpace
{
    [Fact]
    public void WithNullCollection_ThrowsArgumentNullException()
    {
        IEnumerable<string?>? collection = null;

        Should.Throw<ArgumentNullException>(() => collection.AssertItemsNotNullOrWhiteSpace())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionContainingWhiteSpace_ThrowsArgumentException()
    {
        IEnumerable<string?> collection = ["x", " "];

        Should.Throw<ArgumentException>(() => collection.AssertItemsNotNullOrWhiteSpace())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithValidCollection_DoesNotThrow()
    {
        IEnumerable<string?> collection = ["x", "y"];

        collection.AssertItemsNotNullOrWhiteSpace();
    }
}
