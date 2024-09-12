# AsyncHandler.Assembly
### .Net assembly scanning and type discovery made simplified.

[![Github follow](https://img.shields.io/badge/follow-asynchandler-bf9136?logo=github)](https://github.com/asynchandler)
[![Nuget Package](https://badgen.net/nuget/v/asynchandler.Assembly)](https://www.nuget.org/packages/AsyncHandler.Assembly)
[![Nuget](https://badgen.net/nuget/dt/asynchandler.Assembly)](https://www.nuget.org/packages/AsyncHandler.Assembly)
[![Github follow](https://img.shields.io/badge/give_us_a-⭐-yellow?logo=github)](https://github.com/asynchandler/AsyncHandler.Assembly)

<div align="left">
    <img src=".assets/ah_radius.PNG" width="80" height="80" style="float:left;" alt="asynchandler">
</div>

### Overview
`AsyncHandler.Assembly` simplifies searching through .Net assemblies when looking for a specific type with a bunch of helpful static and extensions methods to help speed up development.

### Prerequisities
[![My Skills](https://skillicons.dev/icons?i=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

The library runs on the stable release of .Net 8 and requires the SDK installed:

    https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Install the package

Iinstall `AsyncHandler.Assembly` package.

    dotnet add package AsyncHandler.Assembly
    
### Examples
Search for a derived type with its root.
```csharp
using System.Reflection;
using AsyncHandler.Assembly;

public record AggregateRoot;
public record OrderAggregate : AggregateRoot;

var assembly = Assembly.GetExecutingAssembly();
var type = TDiscover.FindByAsse<AggregateRoot>(assembly);
// or typeof(AggregateRoot).FindByAsse(Assembly.GetExecutingAssembly());
```

Search for a type using calling assembly all the way back to matching assembly. `FindByCallingAsse()` offers significant performance gains.
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
// or typeof(OrderPlaced).FindByTypeName("OrderPlaced");
```
Search for a type when all you have is the type name.

```csharp
TDiscover.FindByTypeName("OrderPlaced");
```

### Give us a ⭐
If you are an event sourcer and love OSS, give [AsyncHandler.Assembly](https://github.com/asynchandler/AsyncHandler.Assembly) a star. :purple_heart:

### License

This project is licensed under the terms of the [MIT](https://github.com/asynchandler/AsyncHandler.Assembly/blob/main/LICENSE) license.