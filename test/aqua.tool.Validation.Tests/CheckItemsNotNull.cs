namespace aqua.tool.Validation.Tests;

public class CheckItemsNotNull
{
    [Fact]
    public void WithNullCollection_ThrowsArgumentNullException()
    {
        object?[]? collection = null;

        Should.Throw<ArgumentNullException>(() => collection.CheckItemsNotNull())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionContainingNull_ThrowsArgumentException()
    {
        object?[] collection = [1, null];

        Should.Throw<ArgumentException>(() => collection.CheckItemsNotNull())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithValidCollection_ReturnsSameInstance()
    {
        object?[] collection = [1, 2];

        collection.CheckItemsNotNull().ShouldBeSameAs(collection);
    }
}
