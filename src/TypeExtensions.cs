namespace AsyncHandler.Asse;

using System.Reflection;
/// <summary>
/// Helper methods to find your type
/// </summary>
public static class TypeExtensions
{
    
    /// <summary>
    /// Searches through the provided assembly.
    /// </summary>
    /// <param name="assembly">The assembly to search through.</param>
    /// <param name="type">The type to search for.</param>
    /// <returns>A Type if one is found or null otherwise</returns>
    public static Type? FindByAsse(this Type type, Assembly assembly)
    {
        return assembly.GetTypes().Where(x => type.IsAssignableFrom(x)).FirstOrDefault();
    }
    
    /// <summary>
    /// Searches through the provided calling assembly, this reverse search starting from
    /// caller results in significant performance gains compared to AppDomain.
    /// </summary>
    /// <param name="caller">Calling assembly i.e. Assembly.GetCallingAssembly().</param>
    /// <param name="type">The type to search for.</param>
    /// <returns>A Type if one is found or null otherwise</returns>
    public static Type? FindByCallingAsse(this Type type, Assembly caller)
    {
        var exists = caller.GetTypes()
        .FirstOrDefault(x => type.IsAssignableFrom(x));
        if(exists != null)
            return exists;
        
        var refs = caller.GetReferencedAssemblies()
        .Where(r => !TDiscover.ExcludedAssemblies.Any(x => r.FullName.StartsWith(x)));

        return refs.Where(x => Assembly.Load(x).GetReferencedAssemblies()
        .Any(r => AssemblyName.ReferenceMatchesDefinition(r, type.Assembly.GetName())))
        .SelectMany(x => Assembly.Load(x).GetTypes())
        .FirstOrDefault(t => type.IsAssignableFrom(t));
    }
    /// <summary>
    /// Searches through the AppDomain for the type argument that matches the specified
    /// type parameter.
    /// use this when you have multiple matches and use the type arugment for an exact match.
    /// </summary>
    /// <param name="type">The type argument to search for.</param>
    /// <param name="typeName">The type argument to search for.</param>
    /// <returns>A Type if there is a match or null otherwise</returns>
    public static Type? FindByTypeName(this Type type, string typeName)
    {
        var asses = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    where assembly.FullName != null &&
                    !TDiscover.ExcludedAssemblies.Any(x => assembly.FullName.StartsWith(x)) &&
                    assembly.ManifestModule.Name != "<In Memory Module>" 
                    select assembly
                    ).ToList();

        var targetDefinition = type.Assembly.GetName();

        return asses.Where(x => x.GetReferencedAssemblies()
        .Any(an => AssemblyName.ReferenceMatchesDefinition(an, targetDefinition)))
        .SelectMany(a => a.GetTypes())
        .FirstOrDefault(t => type.IsAssignableFrom(t) && t.Name == typeName);
    }
}
