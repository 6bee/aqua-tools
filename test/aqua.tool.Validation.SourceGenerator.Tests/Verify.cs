namespace aqua.tool.Validation.SourceGenerator.Tests;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;

public class Verify
{
    [Fact]
    public void DefaultOptions_GeneratesAndExecutesValidationCode()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        CSharpCompilation compilation = CreateCompilation(
            """
            using System;

            public static class Entry
            {
                public static void Run(object? value)
                    => value.AssertNotNull();
            }
            """);

        GeneratorDriver driver = RunGenerator(compilation, null, cancellationToken, out Compilation outputCompilation, out var generatorDiagnostics);

        generatorDiagnostics.Where(d => d.Severity is DiagnosticSeverity.Error).ShouldBeEmpty();

        outputCompilation.GetDiagnostics(cancellationToken).Where(d => d.Severity is DiagnosticSeverity.Error).ShouldBeEmpty();

        driver.GetRunResult()
            .Results
            .SelectMany(r => r.GeneratedSources)
            .Any(s => s.SourceText.ToString().Contains("static class _Check"))
            .ShouldBeTrue();

        using MemoryStream peStream = new();

        EmitResult emitResult = outputCompilation.Emit(peStream, cancellationToken: cancellationToken);

        emitResult.Success.ShouldBeTrue();

        AssemblyLoadContext assemblyLoadContext = new("ValidationGeneratorTests", isCollectible: true);

        peStream.Position = 0;

        Assembly assembly = assemblyLoadContext.LoadFromStream(peStream);

        MethodInfo run = assembly.GetType("Entry")!.GetMethod("Run", BindingFlags.Public | BindingFlags.Static)!;

        var exception = Should.Throw<TargetInvocationException>(() => run.Invoke(null, [null]));

        exception.InnerException.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("value");

        assemblyLoadContext.Unload();
    }

    [Fact]
    public void WithPublicOption_GeneratesSourceSuccessfully()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        CSharpCompilation compilation = CreateCompilation(
            """
            using System;

            public static class Entry
            {
                public static void Run(object? value)
                    => value.AssertNotNull();
            }
            """);

        GeneratorDriver driver = RunGenerator(
            compilation,
            new Dictionary<string, string>
            {
                ["AquaToolValidationPublic"] = "true"
            },
            cancellationToken,
            out Compilation outputCompilation,
            out var generatorDiagnostics);

        generatorDiagnostics.Where(static d => d.Severity is DiagnosticSeverity.Error).ShouldBeEmpty();

        outputCompilation.GetDiagnostics(cancellationToken).Where(static d => d.Severity is DiagnosticSeverity.Error).ShouldBeEmpty();

        driver.GetRunResult()
            .Results
            .SelectMany(static r => r.GeneratedSources)
            .Any(static s => s.SourceText.ToString().Contains("static class _Check"))
            .ShouldBeTrue();
    }

    [Fact]
    public void WithLanguageVersionLowerThan8_DoesNotGenerateSources()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        CSharpCompilation compilation = CreateCompilation(
            """
            public static class Entry
            {
                public static int Run(int value)
                    => value;
            }
            """,
            LanguageVersion.CSharp7_3);

        GeneratorDriver driver = RunGenerator(compilation, null, cancellationToken, out Compilation outputCompilation, out var generatorDiagnostics);

        generatorDiagnostics.Where(static d => d.Severity is DiagnosticSeverity.Error).ShouldBeEmpty();

        outputCompilation.GetDiagnostics(cancellationToken).Where(static d => d.Severity is DiagnosticSeverity.Error).ShouldBeEmpty();

        driver.GetRunResult()
            .Results
            .SelectMany(static r => r.GeneratedSources)
            .ShouldBeEmpty();
    }

    private static GeneratorDriver RunGenerator(
        CSharpCompilation compilation,
        IReadOnlyDictionary<string, string>? msbuildProperties,
        System.Threading.CancellationToken cancellationToken,
        out Compilation outputCompilation,
        out ImmutableArray<Diagnostic> generatorDiagnostics)
    {
        GeneratorDriver driver;

        if (msbuildProperties is null)
        {
            driver = CSharpGeneratorDriver.Create(new aqua.tool.Validation.Generator());
        }
        else
        {
            var globalOptions =
                msbuildProperties.ToDictionary(
                    static pair => $"build_property.{pair.Key}",
                    static pair => pair.Value,
                    StringComparer.Ordinal);

            driver = CSharpGeneratorDriver.Create(
                generators: [new aqua.tool.Validation.Generator().AsSourceGenerator()],
                parseOptions: (CSharpParseOptions?)compilation.SyntaxTrees.First().Options,
                optionsProvider: new TestAnalyzerConfigOptionsProvider(globalOptions));
        }

        return driver.RunGeneratorsAndUpdateCompilation(compilation, out outputCompilation, out generatorDiagnostics, cancellationToken);
    }

    private static CSharpCompilation CreateCompilation(string source, LanguageVersion languageVersion = LanguageVersion.Latest)
    {
        string trustedPlatformAssemblies = (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!;

        MetadataReference[] references =
            trustedPlatformAssemblies
                .Split(Path.PathSeparator)
                .Select(static assemblyPath => MetadataReference.CreateFromFile(assemblyPath))
                .ToArray();

        SyntaxTree syntaxTree =
            CSharpSyntaxTree.ParseText(
                source,
                CSharpParseOptions.Default.WithLanguageVersion(languageVersion));

        return CSharpCompilation.Create(
            assemblyName: "GeneratorTestAssembly",
            syntaxTrees: [syntaxTree],
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
    }

    private sealed class TestAnalyzerConfigOptionsProvider(IReadOnlyDictionary<string, string> globalOptions) : AnalyzerConfigOptionsProvider
    {
        private static readonly AnalyzerConfigOptions Empty = new TestAnalyzerConfigOptions(new Dictionary<string, string>());

        public override AnalyzerConfigOptions GlobalOptions { get; } = new TestAnalyzerConfigOptions(globalOptions);

        public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
        {
            return Empty;
        }

        public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
        {
            return Empty;
        }
    }

    private sealed class TestAnalyzerConfigOptions(IReadOnlyDictionary<string, string> options) : AnalyzerConfigOptions
    {
        public override bool TryGetValue(string key, out string value)
        {
            return options.TryGetValue(key, out value!);
        }
    }
}
