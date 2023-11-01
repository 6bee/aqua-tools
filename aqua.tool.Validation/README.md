# What is it? 🚀

**aqua.tool.Validation** provides C# source generated extension methods for argument validation.

This package is designed to work for various target frameworks and makes use of [PolySharp](https://www.nuget.org/packages/PolySharp) to integrate with different C# language versions.

# How to Use

``` C#
public void SampleMethod(string text)
{
  // Throw an ArgumentNullException if text is null.
  // Throw an ArgumentException if text is empty.
  text.AssertNotNullOrEmpty();
}

public void SampleMethod(IReadOnlyList<string> text)
{
  // Throw an ArgumentNullException if text is null.
  // Throw an ArgumentException if any element in text is null or empty.
  text.AssertItemsNotNullOrEmpty();
}
```

## Assert vs. Check

Most validation methods exists in two flavors. While _assert_ simply verifies the input (e.g. `AssertNotNull`), _check_ also returns the input value to allow for fluent API style code (e.g. `CheckNotNull`).

# Migrate validation code for version 2.2.0 and later

Starting with _aqua.tool.Validation v2.2.0_ the name argument can be omitted as it's atomatically injected by the compiler using the `CallerArgumentExpressionAttribute`.
Existing code can easily be migrated using regex find and replace in Visual Studio:

- Search regex pattern:

  ```RegEx
  \.(?<method>((Assert)|(Check))(Items)?NotNull(OrEmpty)?)\(((nameof\([^\)]+\))|([^\)]+))\)
  ```

- Replace regex pattern:

  ```RegEx
  .${method}()
  ```

  ![migrate validation code](https://raw.githubusercontent.com/6bee/aqua-tools/main/Resources/migrate_validation_code.png)

# Options

Code generation can be configured through some MSBuild properties to set in consuming projects.

| Property                            | Value       | Description                                                                                         |
| :---                                | :---        | :---                                                                                                |
| `AquaToolValidationDisable`         | true\|false | Disable compilation of generated source code.                                                       |
| `AquaToolValidationPublic`          | true\|false | Declare generated source code as public. By default, generated source code has internal visibility. |
| `AquaToolValidationNullableDisable` | true\|false | Suppress nullable attributes.                                                                       |
