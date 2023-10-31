namespace aqua.tool.Validation.Helpers;

using System;
using System.Collections.Immutable;

/// <summary>
/// Extensions for <see cref="EquatableArray{T}"/>.
/// </summary>
internal static class EquatableArrayExtensions
{
    /// <summary>
    /// Creates an <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of items in the input array.</typeparam>
    /// <param name="array">The input <see cref="ImmutableArray{T}"/> instance.</param>
    /// <returns>An <see cref="EquatableArray{T}"/> instance from a given <see cref="ImmutableArray{T}"/>.</returns>
    public static EquatableArray<T> AsEquatableArray<T>(this ImmutableArray<T> array)
        where T : IEquatable<T>
        => new(array);
}
