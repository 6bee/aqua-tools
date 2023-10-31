namespace aqua.tool.Validation.Constants;

/// <summary>
/// Exposes the available MSBuild properties.
/// </summary>
internal static class MSBuildProperties
{
    /// <summary>
    /// The MSBuild property for <see cref="Models.GenerationOptions.UsePublicAccessibilityForGeneratedTypes"/>.
    /// </summary>
    public const string UsePublicAccessibilityForGeneratedTypes = "AquaToolValidationPublic";

    /// <summary>
    /// The MSBuild property for <see cref="Models.GenerationOptions.DisableGeneratedCode"/>.
    /// </summary>
    public const string DisableGeneratedCode = "AquaToolValidationDisable";

    /// <summary>
    /// The MSBuild property for <see cref="Models.GenerationOptions.ExcludeGeneratedTypes"/>.
    /// </summary>
    public const string ExcludeGeneratedTypes = "AquaToolValidationExcludeGeneratedTypes";

    /// <summary>
    /// The MSBuild property for <see cref="Models.GenerationOptions.IncludeGeneratedTypes"/>.
    /// </summary>
    public const string IncludeGeneratedTypes = "AquaToolValidationIncludeGeneratedTypes";
}