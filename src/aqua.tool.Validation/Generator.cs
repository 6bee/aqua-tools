namespace aqua.tool.Validation;

using System.Linq;
using Microsoft.CodeAnalysis;

/// <summary>
/// A source generator injecting all needed C# sources at compile time.
/// </summary>
[Generator(LanguageNames.CSharp)]
public sealed partial class Generator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var generationOptions =
            context.AnalyzerConfigOptionsProvider
            .Select(GetGenerationOptions);

        var availableTypes =
            context.CompilationProvider
            .SelectMany(GetAvailableTypes);

        var generatedTypes =
            availableTypes
            .Combine(generationOptions)
            .Where(IsAvailableTypeSelected)
            .Select(GetGeneratedType);

        context.RegisterSourceOutput(generatedTypes, EmitGeneratedType);
    }
}