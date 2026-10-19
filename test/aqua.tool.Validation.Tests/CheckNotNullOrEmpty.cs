namespace aqua.tool.Validation.Tests;

public class CheckNotNullOrEmpty
{
    [Fact]
    public void WithCollectionClassNull_ThrowsArgumentNullException()
    {
        int[]? collection = null;

        Should.Throw<ArgumentNullException>(() => collection.CheckNotNullOrEmpty())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionClassEmpty_ThrowsArgumentException()
    {
        var collection = Array.Empty<int>();

        Should.Throw<ArgumentException>(() => collection.CheckNotNullOrEmpty())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionClassValue_ReturnsSameInstance()
    {
        var collection = new[] { 1 };

        collection.CheckNotNullOrEmpty().ShouldBeSameAs(collection);
    }

    [Fact]
    public void WithCollectionStructNull_ThrowsArgumentNullException()
    {
        EnumerableStruct? collection = null;

        Should.Throw<ArgumentNullException>(() => collection.CheckNotNullOrEmpty())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionStructEmpty_ThrowsArgumentException()
    {
        EnumerableStruct? collection = new EnumerableStruct(Array.Empty<int>());

        Should.Throw<ArgumentException>(() => collection.CheckNotNullOrEmpty())
            .ParamName.ShouldBe("collection");
    }

    [Fact]
    public void WithCollectionStructValue_ReturnsValue()
    {
        EnumerableStruct? collection = new EnumerableStruct([1]);

        collection.CheckNotNullOrEmpty().Count.ShouldBe(1);
    }

    [Fact]
    public void WithStringNull_ThrowsArgumentNullException()
    {
        string? value = null;

        Should.Throw<ArgumentNullException>(() => value.CheckNotNullOrEmpty())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithStringEmpty_ThrowsArgumentException()
    {
        const string value = "";

        Should.Throw<ArgumentException>(() => value.CheckNotNullOrEmpty())
            .ParamName.ShouldBe("value");
    }

    [Fact]
    public void WithNonEmptyString_ReturnsInput()
    {
        const string value = "x";

        value.CheckNotNullOrEmpty().ShouldBe(value);
    }

    private readonly struct EnumerableStruct(int[] values) : IEnumerable
    {
        private readonly int[] _values = values;

        public int Count => _values.Length;

        public IEnumerator GetEnumerator() => _values.GetEnumerator();
    }
}
