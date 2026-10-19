namespace aqua.tool.Validation.Tests;

public class AssertItemsNotNull
{
    [Fact]
    public void WithNullCollection_ThrowsArgumentNullException()
    {
        IEnumerable<object?>? collection = null;

        Should.Throw<ArgumentNullException>(() => collection.AssertItemsNotNull())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionContainingNull_ThrowsArgumentException()
    {
        IEnumerable<object?> collection = [1, null];

        Should.Throw<ArgumentException>(() => collection.AssertItemsNotNull())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithValidCollection_DoesNotThrow()
    {
        IEnumerable<object?> collection = [1, 2];

        collection.AssertItemsNotNull();
    }
}
