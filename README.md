# AsyncHandler.Assembly
### A .Net library that helps making type discovery faster and simplified. 

[![Github follow](https://img.shields.io/badge/follow-asynchandler-bf9136?logo=github)](https://github.com/asynchandler)
[![Github follow](https://img.shields.io/badge/follow-eventsourcer-bf9136?logo=github)](https://github.com/eventsourcer)
[![Nuget Package](https://badgen.net/nuget/v/asynchandler.Assembly)](https://www.nuget.org/packages/AsyncHandler.Assembly)
[![Nuget](https://badgen.net/nuget/dt/asynchandler.Assembly)](https://www.nuget.org/packages/AsyncHandler.Assembly)
[![Github follow](https://img.shields.io/badge/give_us_a-⭐-yellow?logo=github)](https://github.com/asynchandler/AsyncHandler.Assembly)

<div align="left">
    <img src=".assets/github.png" width="80" height="80" style="float:left;" alt="asynchandler">
</div>

### Overview
`AsyncHandler.Assembly` simplifies type discovery overhead when searching through .Net assemblies with a bunch of helpful methods to speed up your development.

### Prerequisities
[![My Skills](https://skillicons.dev/icons?i=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

The library runs on the stable release of .Net 8 and requires the SDK installed:

    https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Install the package

Iinstall `AsyncHandler.Assembly` package.

    dotnet add package AsyncHandler.Assembly
    
### Examples
Search for a derived type by its root.
```csharp
using System.Reflection;
using AsyncHandler.Assembly;

public record AggregateRoot;
public record OrderAggregate : AggregateRoot;

var assembly = Assembly.GetExecutingAssembly();
var type = TDiscover.FindByAsse<AggregateRoot>(assembly);
// or typeof(AggregateRoot).FindByAsse(assembly);
```

Use `FindByCallingAsse()` to start from calling assembly all the way back to matching assembly, `FindByCallingAsse()` offers significant performance gains.
```csharp
typeof(AggregateRoot).FindByCallingAsse(Assembly.GetCallingAssembly());
```

Search for a type through `AppDomain`, smart tricks and filters are applied to enhance the search.
```csharp
public record DomainEvent;
public record OrderPlaced : DomainEvent;

TDiscover.FindByType<DomainEvent>();
```

To further enhance the above search, use `FindByTypeName` to specify the type and name as well.
```csharp
public record DomainEvent;
public record OrderPlaced : DomainEvent;
public record OrderConfirmed : DomainEvent;

TDiscover.FindByTypeName<DomainEvent>("OrderPlaced");
// or typeof(DomainEvent).FindByTypeName("OrderPlaced");
```
Search for a type when all you have is the type name.

```csharp
TDiscover.FindByTypeName("OrderPlaced");
```

### Give us a ⭐
If you are an assembly and typing guru, give [AsyncHandler.Assembly](https://github.com/asynchandler/AsyncHandler.Assembly) a star. :purple_heart:

### License

This project is licensed under the terms of the [MIT](https://github.com/asynchandler/AsyncHandler.Assembly/blob/main/LICENSE) license.