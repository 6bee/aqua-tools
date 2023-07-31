﻿// <auto-generated>
//   This code file has automatically been generated by the "aqua.tool.Validaton" NuGet package (https://www.nuget.org/packages/aqua.tool.Validation).
//   Copyright (c) Christof Senn. See license.txt in the package root for license information.
// </auto-generated>

#nullable enable
#pragma warning disable

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

[EditorBrowsable(EditorBrowsableState.Never)]
[DebuggerNonUserCode]
[DebuggerStepThrough]
[GeneratedCode("aqua.tool.Validation", "")]
[SuppressMessage(
    "Major Bug",
    "S3903:Types should be defined in named namespaces",
    Justification = "Global extension method with internal visibility.")]
internal static class _Check
{
    private const MethodImplOptions AggressiveInlining = (MethodImplOptions)256;

    /// <summary>
    ///   Throws an <see cref="ArgumentNullException"/> if <paramref name="value"/> is <see langword="null"/>,
    ///   otherwise the <paramref name="value"/> is returned.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
    /// <returns>The <paramref name="value"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static T CheckNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this T? value,
        string name)
        => value ?? throw new ArgumentNullException(name);

    /// <summary>
    ///   Throws an <see cref="ArgumentNullException"/> if <paramref name="value"/> is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <see langword="null"/>.</exception>
    [MethodImpl(AggressiveInlining)]
    public static void AssertNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this T? value,
        string name)
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }
    }

    /// <summary>
    ///   Throws if <paramref name="items"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    /// <returns>The <paramref name="items"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IEnumerable<T> CheckNotNullOrEmpty<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IEnumerable<T>? items,
        string name)
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (!items.Any())
        {
            throw new ArgumentException("Collection must not be empty.", name);
        }

        return items;
    }

    /// <summary>
    ///   Throws if <paramref name="items"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    /// <returns>The <paramref name="items"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlyCollection<T> CheckNotNullOrEmpty<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlyCollection<T>? items,
        string name)
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (!items.Any())
        {
            throw new ArgumentException("Collection must not be empty.", name);
        }

        return items;
    }

    /// <summary>
    ///   Throws if <paramref name="items"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    /// <returns>The <paramref name="items"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlyList<T> CheckNotNullOrEmpty<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlyList<T>? items,
        string name)
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (!items.Any())
        {
            throw new ArgumentException("Collection must not be empty.", name);
        }

        return items;
    }

    /// <summary>
    ///   Throws if <paramref name="items"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    /// <returns>The <paramref name="items"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlyDictionary<TKey, TValue> CheckNotNullOrEmpty<TKey, TValue>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlyDictionary<TKey, TValue>? dict,
        string name)
    {
        if (dict is null)
        {
            throw new ArgumentNullException(name);
        }

        if (!dict.Any())
        {
            throw new ArgumentException("Collection must not be empty.", name);
        }

        return dict;
    }

#if NET5_0_OR_GREATER
    /// <summary>
    ///   Throws if <paramref name="items"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    /// <returns>The <paramref name="items"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlySet<T> CheckNotNullOrEmpty<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlySet<T>? set,
        string name)
    {
        if (set is null)
        {
            throw new ArgumentNullException(name);
        }

        if (!set.Any())
        {
            throw new ArgumentException("Collection must not be empty.", name);
        }

        return set;
    }
#endif // NET5_0_OR_GREATER

    /// <summary>
    ///   Throws if <paramref name="value"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    /// <returns>The <paramref name="items"/> unless it's <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static string CheckNotNullOrEmpty(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this string? value,
        string name)
    {
        if (value is null)
        {
            throw new ArgumentNullException(name);
        }

        if (value.Length == 0)
        {
            throw new ArgumentException("String must not be empty.", name);
        }

        return value;
    }

    /// <summary>
    ///   Throws if <paramref name="items"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="items"/> is empty.</exception>
    [MethodImpl(AggressiveInlining)]
    public static void AssertNotNullOrEmpty<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IEnumerable<T>? items,
        string name)
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (!items.Any())
        {
            throw new ArgumentException($"{(items is string ? "String" : "Collection")} must not be empty.", name);
        }
    }

    /// <summary>
    ///   Throws if either <paramref name="items"/> or any element contained is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If any element in <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <returns>The <paramref name="items"/> unless it or any element contained is <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IEnumerable<T> CheckItemsNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IEnumerable<T>? items,
        string name)
        where T : class
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (items.Any(x => x is null))
        {
            throw new ArgumentException("Collection must not contain any null items.", name);
        }

        return items;
    }

    /// <summary>
    ///   Throws if either <paramref name="items"/> or any element contained is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If any element in <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <returns>The <paramref name="items"/> unless it or any element contained is <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlyCollection<T> CheckItemsNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlyCollection<T>? items,
        string name)
        where T : class
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (items.Any(x => x is null))
        {
            throw new ArgumentException("Collection must not contain any null items.", name);
        }

        return items;
    }

    /// <summary>
    ///   Throws if either <paramref name="items"/> or any element contained is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If any element in <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <returns>The <paramref name="items"/> unless it or any element contained is <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlyList<T> CheckItemsNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlyList<T>? items,
        string name)
        where T : class
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (items.Any(x => x is null))
        {
            throw new ArgumentException("Collection must not contain any null items.", name);
        }

        return items;
    }

#if NET5_0_OR_GREATER
    /// <summary>
    ///   Throws if either <paramref name="items"/> or any element contained is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If any element in <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <returns>The <paramref name="items"/> unless it or any element contained is <see langword="null"/>.</returns>
    [MethodImpl(AggressiveInlining)]
    public static IReadOnlySet<T> CheckItemsNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IReadOnlySet<T>? items,
        string name)
        where T : class
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (items.Any(x => x is null))
        {
            throw new ArgumentException("Collection must not contain any null items.", name);
        }

        return items;
    }
#endif // NET5_0_OR_GREATER

    /// <summary>
    ///   Throws if either <paramref name="items"/> or any element contained is <see langword="null"/>.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If any element in <paramref name="items"/> is <see langword="null"/>.</exception>
    [MethodImpl(AggressiveInlining)]
    public static void AssertItemsNotNull<T>(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IEnumerable<T>? items,
        string name)
        where T : class
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (items.Any(x => x is null))
        {
            throw new ArgumentException("Collection must not contain any null items.", name);
        }
    }

    /// <summary>
    ///   Throws if either <paramref name="items"/> or any element contained is <see langword="null"/> or empty.
    /// </summary>
    /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If any element in <paramref name="items"/> is <see langword="null"/> or empty.</exception>
    [MethodImpl((MethodImplOptions)AggressiveInlining)]
    public static void AssertItemsNotNullOrEmpty(
#if !NULLABLE_ATTRIBUTES_DISABLE
        [ValidatedNotNull]
#endif // NULLABLE_ATTRIBUTES_DISABLE
        this IEnumerable<string>? items,
        string name)
    {
        if (items is null)
        {
            throw new ArgumentNullException(name);
        }

        if (items.Any(string.IsNullOrEmpty))
        {
            throw new ArgumentException("Collection must not contain any null or empty strings.", name);
        }
    }
}

#pragma warning restore
#nullable restore