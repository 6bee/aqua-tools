using aqua.tool.Validation.Helpers;

namespace aqua.tool.Validation.Models;

/// <summary>
/// A model to hold all generation options for the source generator.
/// </summary>
/// <param name="DisableGeneratedCode">Whether disable compilation of generated source code.</param>
/// <param name="UseEmbeddedAttribute">Whether generated code is "embedded", meaning that even if <c>InternalsVisibleTo</c> is used, the generated types are still not visible to the other assemblies.</param>
/// <param name="UsePublicAccessibilityForGeneratedTypes">Whether to use public accessibility for the generated types.</param>
/// <param name="ExcludeGeneratedTypes">The collection of fully qualified type names of types to exclude from generation.</param>
/// <param name="IncludeGeneratedTypes">The collection of fully qualified type names of types to include in the generation.</param>
internal sealed record GenerationOptions(
    bool DisableGeneratedCode,
    bool UseEmbeddedAttribute,
    bool UsePublicAccessibilityForGeneratedTypes,
    EquatableArray<string> ExcludeGeneratedTypes,
    EquatableArray<string> IncludeGeneratedTypes);