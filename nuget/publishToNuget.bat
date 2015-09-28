nuget pack -sym ..\DynamiConf\DynamiConf.csproj -Prop Configuration=Release -Prop Platform=AnyCPU
nuget pack -sym ..\DynamiConf.AzureLocator\DynamiConf.AzureLocator.csproj -Prop Configuration=Release -Prop Platform=AnyCPU
nuget pack -sym ..\DynamiConf.IniInterpreter\DynamiConf.IniInterpreter.csproj -Prop Configuration=Release -Prop Platform=AnyCPU
nuget pack -sym ..\DynamiConf.JsonInterpreter\DynamiConf.JsonInterpreter.csproj -Prop Configuration=Release -Prop Platform=AnyCPU
nuget pack -sym ..\DynamiConf.HttpLocator\DynamiConf.HttpLocator.csproj -Prop Configuration=Release -Prop Platform=AnyCPU