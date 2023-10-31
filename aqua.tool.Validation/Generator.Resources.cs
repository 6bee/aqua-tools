namespace aqua.tool.Validation;

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using aqua.tool.Validation.Constants;
using aqua.tool.Validation.Extensions;
using aqua.tool.Validation.Helpers;
using aqua.tool.Validation.Models;
using Microsoft.CodeAnalysis.CSharp;

/// <inheritdoc/>
partial class Generator
{
    /// <summary>
    /// A regex to extract the fully qualified type name of a type from its embedded resource name.
    /// </summary>
    private const string EmbeddedResourceNameToFullyQualifiedTypeNameRegex = @"^aqua\.tool\.Validation\.EmbeddedResources(?:\.RuntimeSupported)?\.(System(?:\.\w+)+)\.cs$";

    /// <summary>
    /// The mapping of fully qualified type names to embedded resource names.
    /// </summary>
    public static readonly ImmutableDictionary<string, string> FullyQualifiedTypeNamesToResourceNames = ImmutableDictionary.CreateRange(
        from string resourceName in typeof(Generator).Assembly.GetManifestResourceNames()
        select new KeyValuePair<string, string>(Regex.Match(resourceName, EmbeddedResourceNameToFullyQualifiedTypeNameRegex).Groups[1].Value, resourceName));

    /// <summary>
    /// The collection of fully qualified type names for language support types.
    /// </summary>
    private static readonly ImmutableArray<string> LanguageSupportTypeNames = ImmutableArray.CreateRange(
        from string resourceName in typeof(Generator).Assembly.GetManifestResourceNames()
        where !resourceName.StartsWith("aqua.tool.Validation.EmbeddedResources.RuntimeSupported.")
        select Regex.Match(resourceName, EmbeddedResourceNameToFullyQualifiedTypeNameRegex).Groups[1].Value);

    /// <summary>
    /// The collection of fully qualified type names for runtime supported types.
    /// </summary>
    private static readonly ImmutableArray<string> RuntimeSupportedTypeNames = ImmutableArray.CreateRange(
        from string resourceName in typeof(Generator).Assembly.GetManifestResourceNames()
        where resourceName.StartsWith("aqua.tool.Validation.EmbeddedResources.RuntimeSupported.")
        select Regex.Match(resourceName, EmbeddedResourceNameToFullyQualifiedTypeNameRegex).Groups[1].Value);

    /// <summary>
    /// The collection of all fully qualified type names for available polyfill types.
    /// </summary>
    private static readonly ImmutableArray<string> AllSupportTypeNames = ImmutableArray.CreateRange(LanguageSupportTypeNames.Concat(RuntimeSupportedTypeNames));

    /// <summary>
    /// The <see cref="Regex"/> to find all <see cref="System.Runtime.CompilerServices.MethodImplOptions"/> uses.
    /// </summary>
    private static readonly Regex MethodImplOptionsRegex = new(@" *\[global::System\.Runtime\.CompilerServices\.MethodImpl\(global::System\.Runtime\.CompilerServices\.MethodImplOptions\.AggressiveInlining\)\]\r?\n", RegexOptions.Compiled);

    /// <summary>
    /// The <see cref="Regex"/> to find all <see cref="System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"/> uses.
    /// </summary>
    private static readonly Regex ExcludeFromCodeCoverageRegex = new(@" *\[global::System\.Diagnostics\.CodeAnalysis\.ExcludeFromCodeCoverage\]\r?\n", RegexOptions.Compiled);

    /// <summary>
    /// The dictionary of cached sources to produce.
    /// </summary>
    private readonly ConcurrentDictionary<GeneratedType, SourceText> _manifestSources = new();

    /// <summary>
    /// Extracts the <see cref="GenerationOptions"/> value for the current generation.
    /// </summary>
    /// <param name="options">The input <see cref="AnalyzerConfigOptionsProvider"/> instance.</param>
    /// <param name="_">The cancellation token for the operation.</param>
    /// <returns>The <see cref="GenerationOptions"/> for the current generation.</returns>
    private static GenerationOptions GetGenerationOptions(AnalyzerConfigOptionsProvider options, CancellationToken _)
    {
        var usePublicAccessibilityForGeneratedTypes = options.GetBoolMSBuildProperty(MSBuildProperties.UsePublicAccessibilityForGeneratedTypes);

        var disableGeneratedCode = options.GetBoolMSBuildProperty(MSBuildProperties.DisableGeneratedCode);

        var excludeGeneratedTypes = options.GetStringArrayMSBuildProperty(MSBuildProperties.ExcludeGeneratedTypes);

        var includeGeneratedTypes = options.GetStringArrayMSBuildProperty(MSBuildProperties.IncludeGeneratedTypes);

        return new(
            usePublicAccessibilityForGeneratedTypes,
            disableGeneratedCode,
            excludeGeneratedTypes,
            includeGeneratedTypes);
    }

    /// <summary>
    /// Calculates the collection of <see cref="AvailableType"/> that could be generated.
    /// </summary>
    /// <param name="compilation">The current <see cref="Compilation"/> instance.</param>
    /// <param name="token">The cancellation token for the operation.</param>
    /// <returns>The collection of <see cref="AvailableType"/> that could be generated.</returns>
    private static ImmutableArray<AvailableType> GetAvailableTypes(Compilation compilation, CancellationToken token)
    {
        // A minimum of C# 8.0 is required to benefit from the polyfills
        if (!compilation.HasLanguageVersionAtLeastEqualTo(LanguageVersion.CSharp8))
        {
            return ImmutableArray<AvailableType>.Empty;
        }

        // Helper function to check whether a type is available
        static bool IsTypeAvailable(Compilation compilation, string name, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();

            // First check whether the type is accessible, and if it is already then there is nothing left to do
            if (compilation.HasAccessibleTypeWithMetadataName(name))
            {
                return false;
            }

            return true;
        }

        // Helper to get the syntax fixup to apply to a given type
        static SyntaxFixupType GetSyntaxFixupType(Compilation compilation, string name)
        {
            SyntaxFixupType fixupType = SyntaxFixupType.None;

            // Strip the [ExcludeFromCodeCoverage] uses if the target framework doesn't have the type or cannot access it.
            // We can't just check whether the type exists, as on .NET Standard 1.3 projects it might exist but be internal.
            if (!compilation.HasAccessibleTypeWithMetadataName("System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverageAttribute"))
            {
                fixupType |= SyntaxFixupType.RemoveExcludeFromCodeCoverageAttributes;
            }

            return fixupType;
        }

        using ImmutableArrayBuilder<AvailableType> builder = ImmutableArrayBuilder<AvailableType>.Rent();

        // Inspect all available types and filter them down according to the current compilation
        foreach (string name in AllSupportTypeNames)
        {
            if (IsTypeAvailable(compilation, name, token))
            {
                builder.Add(new AvailableType(name, GetSyntaxFixupType(compilation, name)));
            }
        }

        return builder.ToImmutable();
    }

    /// <summary>
    /// Checks whether a type with a specific fully qualified name is selected for generation.
    /// </summary>
    /// <param name="info">The input info for the current generation.</param>
    /// <returns>Whether the current type is selected for generation</returns>
    private static bool IsAvailableTypeSelected((string FullyQualifiedTypeName, GenerationOptions Options) info)
    {
        bool isExplicitlyIncluded = info.Options.IncludeGeneratedTypes.AsImmutableArray().Contains(info.FullyQualifiedTypeName);
        bool isExplicitlyExcluded = info.Options.ExcludeGeneratedTypes.AsImmutableArray().Contains(info.FullyQualifiedTypeName);

        // If the explicit list of types to generate isn't empty, take it into account.
        // Types will be generated only if explicitly requested and not explicitly excluded.
        if (info.Options.IncludeGeneratedTypes.Length > 0)
        {
            return isExplicitlyIncluded && !isExplicitlyExcluded;
        }

        // If there is no list of explicit types, still ignore types that are explicitly excluded
        if (isExplicitlyExcluded)
        {
            return false;
        }

        return LanguageSupportTypeNames.Contains(info.FullyQualifiedTypeName);
    }

    /// <summary>
    /// Checks whether a given <see cref="AvailableType"/> is selected for generation.
    /// </summary>
    /// <param name="info">The input info for the current generation.</param>
    /// <returns>Whether the current <see cref="AvailableType"/> is selected for generation</returns>
    private static bool IsAvailableTypeSelected((AvailableType AvailableType, GenerationOptions Options) info)
    {
        return IsAvailableTypeSelected((info.AvailableType.FullyQualifiedMetadataName, info.Options));
    }

    /// <summary>
    /// Creates a final <see cref="GeneratedType"/> model for generation.
    /// </summary>
    /// <param name="info">The input info for the current generation.</param>
    /// <param name="token">The cancellation token for the operation.</param>
    /// <returns>A <see cref="GeneratedType"/> model for generation.</returns>
    private static GeneratedType GetGeneratedType((AvailableType AvailableType, GenerationOptions Options) info, CancellationToken token)
    {
        // Helper to update the syntax fixup with generation options
        static SyntaxFixupType GetSyntaxFixupType(AvailableType availableType, GenerationOptions options)
        {
            return SyntaxFixupType.None;
        }

        // Combine the syntax type with any analyzer options
        SyntaxFixupType fixupType = info.AvailableType.FixupType | GetSyntaxFixupType(info.AvailableType, info.Options);

        return new(info.AvailableType.FullyQualifiedMetadataName, info.Options.UsePublicAccessibilityForGeneratedTypes, fixupType);
    }

    /// <summary>
    /// Emits the source for a given <see cref="GeneratedType"/> object.
    /// </summary>
    /// <param name="context">The input <see cref="SourceProductionContext"/> instance to use to emit code.</param>
    /// <param name="type">The <see cref="GeneratedType"/> object with info on the source to emit.</param>
    private void EmitGeneratedType(SourceProductionContext context, GeneratedType type)
    {
        // Get the source text from the cache, or load it if needed
        if (!_manifestSources.TryGetValue(type, out SourceText? sourceText))
        {
            string resourceName = FullyQualifiedTypeNamesToResourceNames[type.FullyQualifiedMetadataName];

            using Stream stream = typeof(Generator).Assembly.GetManifestResourceStream(resourceName);

            // If public accessibility has been requested or a syntax fixup is needed, we need to update the loaded source files
            if (type is { IsPublicAccessibilityRequired: true } or { FixupType: not SyntaxFixupType.None })
            {
                string adjustedSource;

                using (StreamReader reader = new(stream))
                {
                    adjustedSource = reader.ReadToEnd();
                }

                if (type.IsPublicAccessibilityRequired)
                {
                    // After reading the file, replace all internal keywords with public. Use a space before and after the identifier
                    // to avoid potential false positives. This could also be done by loading the source tree and using a syntax
                    // rewriter, or just by retrieving the type declaration syntax and updating the modifier tokens, but since the
                    // change is so minimal, it can very well just be done this way to keep things simple, that's fine in this case.
                    adjustedSource = adjustedSource.Replace(" internal ", " public ");
                }

                if ((type.FixupType & SyntaxFixupType.RemoveMethodImplAttributes) != 0)
                {
                    // Just use a regex to remove the attribute. We could use a SyntaxRewriter, but we don't really have that many
                    // cases to handle for now, so once again we can just use the simplest approach for the time being, that's fine.
                    adjustedSource = MethodImplOptionsRegex.Replace(adjustedSource, "");
                }

                if ((type.FixupType & SyntaxFixupType.RemoveExcludeFromCodeCoverageAttributes) != 0)
                {
                    // Just use a regex to remove the attribute, same as in the case above
                    adjustedSource = ExcludeFromCodeCoverageRegex.Replace(adjustedSource, "");
                }

                if ((type.FixupType & SyntaxFixupType.UseInteropServices2ForUnmanagedCallersOnlyAttribute) != 0)
                {
                    // Update the namespace and add the type alias
                    adjustedSource = adjustedSource.Replace(
                        "System.Runtime.InteropServices",
                        "System.Runtime.InteropServices2");
                }

                sourceText = SourceText.From(adjustedSource, Encoding.UTF8);
            }
            else
            {
                // If the default accessibility is used, we can load the source directly
                sourceText = SourceText.From(stream, Encoding.UTF8, canBeEmbedded: true);
            }

            // Cache the generated source (if we raced against another thread, just discard the result)
            _ = _manifestSources.TryAdd(type, sourceText);
        }

        // Finally generate the source text
        context.AddSource($"{type.FullyQualifiedMetadataName}.g.cs", sourceText);
    }
}