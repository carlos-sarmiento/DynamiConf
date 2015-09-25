nuget pack -sym ..\DynamiConf\DynamiConf.csproj -Prop Configuration=Release -Prop Platform=AnyCPU
nuget push DynamiConf.0.1.0.0.nupkg