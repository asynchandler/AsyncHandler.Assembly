namespace AsyncHandler.Asse;

using System.Reflection;
/// <summary>
/// Helper methods to find your type
/// </summary>
public static class TDiscover
{
    /// <summary>
    /// Exclude assemblies to enhance performance
    /// </summary>
    public static IEnumerable<string> ExcludedAssemblies => ["Microsoft", "System", "Swashbuckle"];
    /// <summary>
    /// Searches through the provided assembly.
    /// </summary>
    /// <param name="assembly">The assembly to search through.</param>
    /// <typeparam name="T">The type to search for.</typeparam>
    /// <returns>A Type if one is found or null otherwise</returns>
    public static Type? FindByAsse<T>(Assembly assembly)
    {
        return assembly.GetTypes().Where(x => typeof(T).IsAssignableFrom(x)).FirstOrDefault();
    }
    
    /// <summary>
    /// Searches through the provided calling assembly, this reverse search starting from
    /// caller results in significant performance gains compared to AppDomain.
    /// </summary>
    /// <param name="caller">Calling assembly i.e. Assembly.GetCallingAssembly().</param>
    /// <typeparam name="T">The type to search for a match.</typeparam>
    /// <returns>A Type if one is found or null otherwise</returns>
    public static Type? FindByCallingAsse<T>(Assembly caller)
    {
        var type = caller.GetTypes()
        .FirstOrDefault(x => typeof(T).IsAssignableFrom(x));
        if(type != null)
            return type;
        
        var refs = caller.GetReferencedAssemblies()
        .Where(r => !ExcludedAssemblies.Any(x => r.FullName.StartsWith(x)));

        return refs.Where(x => Assembly.Load(x).GetReferencedAssemblies()
        .Any(r => AssemblyName.ReferenceMatchesDefinition(r, typeof(T).Assembly.GetName())))
        .SelectMany(x => Assembly.Load(x).GetTypes())
        .FirstOrDefault(t => typeof(T).IsAssignableFrom(t));
    }
    /// <summary>
    /// Searches through the AppDomain for the specified type argument , some smart tricks
    /// and filters are applied to boost performance.
    /// this will return the first match among multiple matches found, if any.
    /// </summary>
    /// <typeparam name="T">The type to search for a match.</typeparam>
    /// <returns>A Type if there is a match or null otherwise</returns>
    public static Type? FindByType<T>()
    {
        var asses = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    where assembly.FullName != null &&
                    !ExcludedAssemblies.Any(x => assembly.FullName.StartsWith(x)) &&
                    assembly.ManifestModule.Name != "<In Memory Module>" 
                    select assembly
                    ).ToList();

        var targetDefinition = typeof(T).Assembly.GetName();

        return asses.Where(x => x.GetReferencedAssemblies()
        .Any(an => AssemblyName.ReferenceMatchesDefinition(an, targetDefinition)))
        .SelectMany(a => a.GetTypes())
        .FirstOrDefault(t => typeof(T).IsAssignableFrom(t));
    }
    /// <summary>
    /// Searches through the AppDomain for the type argument that matches the specified
    /// type parameter.
    /// use this when you have multiple matches and use the type arugment for an exact match.
    /// </summary>
    /// <param name="typeName">The type argument to search for.</param>
    /// <typeparam name="T">The type parameter to search for a match.</typeparam>
    /// <returns>A Type if there is a match or null otherwise</returns>
    public static Type? FindByTypeName<T>(string typeName)
    {
        var asses = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    where assembly.FullName != null &&
                    !ExcludedAssemblies.Any(x => assembly.FullName.StartsWith(x)) &&
                    assembly.ManifestModule.Name != "<In Memory Module>" 
                    select assembly
                    ).ToList();

        var targetDefinition = typeof(T).Assembly.GetName();

        return asses.Where(x => x.GetReferencedAssemblies()
        .Any(an => AssemblyName.ReferenceMatchesDefinition(an, targetDefinition)))
        .SelectMany(a => a.GetTypes())
        .FirstOrDefault(t => typeof(T).IsAssignableFrom(t) && t.Name == typeName);
    }
    
    /// <summary>
    /// Searches through the AppDomain for the type argument specified.
    /// use this when you want you search for a type name.
    /// </summary>
    /// <param name="typeName">The type argument to search for.</param>
    /// <returns>A Type if there is a match or null otherwise</returns>
    public static Type? FindByTypeName(string typeName)
    {
        var asses = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    where assembly.FullName != null &&
                    !ExcludedAssemblies.Any(x => assembly.FullName.StartsWith(x)) &&
                    assembly.ManifestModule.Name != "<In Memory Module>" 
                    select assembly
                    ).ToList();

        return asses.SelectMany(a => a.GetTypes())
        .FirstOrDefault(t => t.Name == typeName);
    }
}
