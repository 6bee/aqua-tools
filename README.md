# aqua-tools

| tool                                                                                     | package                                                 |
| :---                                                                                     | :---                                                    |
| [aqua.tool.Validation](#aquatoolvalidation)                                              | [![NuGet Badge][1]][2] [![MyGet Pre Release][3]][4]     |
| [aqua.tool.polyfill.CallerArgumentExpression](#aquatoolpolyfillcallerargumentexpression) | [![NuGet Badge][5]][6] [![MyGet Pre Release][7]][8]     |
| [aqua.tool.polyfill.IsExternalInit](#aquatoolpolyfillisexternalinit)                     | [![NuGet Badge][9]][10] [![MyGet Pre Release][11]][12]  |
| [aqua.tool.polyfill.Nullable](#aquatoolpolyfillnullable)                                 | [![NuGet Badge][13]][14] [![MyGet Pre Release][15]][16] |
| [aqua.tool.polyfill.RequiresPreviewFeatures](#aquatoolpolyfillrequirespreviewfeatures)   | [![NuGet Badge][17]][18] [![MyGet Pre Release][19]][20] |

## aqua.tool.Validation

Provides various extension methods that allow for argument assertiions.

Sample:

``` C#
public void Method(string argument)
{
  // Throw an ArgumentNullException if argument is null.
  // Throw an ArgumentException if argument is empty.
  argument.AssertNotNullOrEmpty();
}
```

### Migrate validation code for version 2.2.0 and later

Starting with _aqua.tool.Validation v2.2.0_ the name argument can be omitted as it's atomatically injected by the compiler.
Existing code can easily be migrated using regex find and replace in Visual Studio:

- search regex pattern:

  ```RegEx
  \.(?<method>((Assert)|(Check))(Items)?NotNull(OrEmpty)?)\(((nameof\([^\)]+\))|([^\)]+))\)
  ```

- replace regex pattern:

  ```RegEx
  .${method}()
  ```

![migrate validation code](Resources/migrate_validation_code.png)

## aqua.tool.polyfill.CallerArgumentExpression

## aqua.tool.polyfill.IsExternalInit

## aqua.tool.polyfill.Nullable

## aqua.tool.polyfill.RequiresPreviewFeatures

[1]: https://buildstats.info/nuget/aqua.tool.Validation?includePreReleases=true
[2]: http://www.nuget.org/packages/aqua.tool.Validation
[3]: http://img.shields.io/myget/aqua/vpre/aqua.tool.Validation.svg?style=flat-square&label=myget
[4]: https://www.myget.org/feed/aqua/package/nuget/aqua.tool.Validation

[5]: https://buildstats.info/nuget/aqua.tool.polyfill.CallerArgumentExpression?includePreReleases=true
[6]: http://www.nuget.org/packages/aqua.tool.polyfill.CallerArgumentExpression
[7]: http://img.shields.io/myget/aqua/vpre/aqua.tool.polyfill.CallerArgumentExpression.svg?style=flat-square&label=myget
[8]: https://www.myget.org/feed/aqua/package/nuget/aqua.tool.polyfill.CallerArgumentExpression

[9]: https://buildstats.info/nuget/aqua.tool.polyfill.IsExternalInit?includePreReleases=true
[10]: http://www.nuget.org/packages/aqua.tool.polyfill.IsExternalInit
[11]: http://img.shields.io/myget/aqua/vpre/aqua.tool.polyfill.IsExternalInit.svg?style=flat-square&label=myget
[12]: https://www.myget.org/feed/aqua/package/nuget/aqua.tool.polyfill.IsExternalInit

[13]: https://buildstats.info/nuget/aqua.tool.polyfill.Nullable?includePreReleases=true
[14]: http://www.nuget.org/packages/aqua.tool.polyfill.Nullable
[15]: http://img.shields.io/myget/aqua/vpre/aqua.tool.polyfill.Nullable.svg?style=flat-square&label=myget
[16]: https://www.myget.org/feed/aqua/package/nuget/aqua.tool.polyfill.Nullable

[17]: https://buildstats.info/nuget/aqua.tool.polyfill.RequiresPreviewFeatures?includePreReleases=true
[18]: http://www.nuget.org/packages/aqua.tool.polyfill.RequiresPreviewFeatures
[19]: http://img.shields.io/myget/aqua/vpre/aqua.tool.polyfill.RequiresPreviewFeatures.svg?style=flat-square&label=myget
[20]: https://www.myget.org/feed/aqua/package/nuget/aqua.tool.polyfill.RequiresPreviewFeatures
