# tdiscover
### A .Net library to help speed up and simplify type discovery. 

[![Github follow](https://img.shields.io/badge/follow-eventstorage-bf9136?logo=github)](https://github.com/eventstorage)
[![Nuget Package](https://badgen.net/nuget/v/TDiscover)](https://www.nuget.org/packages/TDiscover)
[![Nuget](https://badgen.net/nuget/dt/TDiscover)](https://www.nuget.org/packages/TDiscover)
[![Github follow](https://img.shields.io/badge/give_us_a-⭐-yellow?logo=github)](https://github.com/eventstorage/tdiscover)

<div align="left">
    <img src=".assets/github.PNG" width="80" height="80" style="float:left;" alt="asynchandler">
</div>

### Overview
tdiscover simplifies type discovery overhead when searching through .Net assemblies with a bunch of helpful methods to speed up your development.

### Prerequisities
[![My Skills](https://skillicons.dev/icons?i=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

`tdiscover` runs on the stable release of .Net 8 and requires the SDK installed.

    https://dotnet.microsoft.com/en-us/download/dotnet/8.0

### Install the package

Iinstall `TDiscover` package.

    dotnet add package TDiscover
    
### Examples
Search for a derived type by its root.
```csharp
using System.Reflection;
using TDiscover;

public record AggregateRoot;
public record OrderAggregate : AggregateRoot;

var assembly = Assembly.GetExecutingAssembly();
var type = Td.FindByAsse<AggregateRoot>(assembly);
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

Td.FindByType<DomainEvent>();
```

To further enhance the above search, use `FindByTypeName` to specify the type and name as well.
```csharp
public record DomainEvent;
public record OrderPlaced : DomainEvent;
public record OrderConfirmed : DomainEvent;

Td.FindByTypeName<DomainEvent>("OrderPlaced");
// or typeof(DomainEvent).FindByTypeName("OrderPlaced");
```
Search for a type when all you have is the type name.

```csharp
Td.FindByTypeName("OrderPlaced");
```

### Give us a ⭐
If you are an assembly and typing guru, give [tdiscover](https://github.com/eventstorage/tdiscover) a star. :purple_heart:

### License

This project is licensed under the terms of [MIT](https://github.com/eventstorage/tdiscover/blob/main/LICENSE) license.