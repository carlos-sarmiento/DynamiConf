# DynamiConf
Read configuration from many sources at once, merge them seamlessly:
```
dynamic config = new DynamiConfiguration()
                       .MergeWith.IniFormat().From.EmbeddedResources()
                       .MergeWith.EnvironmentVars("var1", "var2")
                       .MergeWith.Json().From.AzureBlockBlob(
                          connectionString: currentConf => currentConf.Azure.ConnectionString,
                          container: currentConf => currentConf.Azure.ConfigurationContainer,
                          blobName: currentConf => currentConf.Azure.BlobName,
                          optional: true)
                       .GetConfiguration();
```

### How to install
```
Package-Install DynamiConf
```

### Pluggable Interpreters
Need to read configuration stored in several formats? Using ``Interpreters``
DynamiConf can read any possible format available. Reading and merging
configuration from sources as disparate as ``Json``, ``Ini files``, ``EnvironmentVars``,
``AppSettings`` and ``Command Line Arguments`` is simple.

See the list of
[Available Interpreters](https://github.com/carlos-sarmiento/DynamiConf/wiki/Available-Interpreters)

### Pluggable Locators
Need to read configuration from a new source? With ``Locators`` DynamiConf can
find configurations anywhere .NET can reach including Embedded Resources,
filesystem and even Azure Blobs. ``Locators`` will pass the loaded data to
interpreters so you can mix and match locations and file formats.

See the list of
[Available Locators](https://github.com/carlos-sarmiento/DynamiConf/wiki/Available-Locators)

### Multiple configurations in a single application
Nothing exists in a shared state, so different components of an application can use
their own configuration sources, eliminating large unmanageable configuration files.
You can even use your dependency injection framework to decide which configuration
to use for each resolution.

But, if you want to share your config with the whole application, the simple
``StaticConf`` class is all you need:

```
StaticConf.Configuration = new DynamiConfiguration()
                                 .MergeWith.Json().From.String("{ 'key' : 'value' }")
                                 .GetConfiguration();

// From anywhere in the AppDomain
var keysValue = StaticConf.Configuration.key;
```

### Simple dependencies
The project is released as separate NuGet packages and the main library has no external dependencies.
You get the dependencies you need with the package you need.
