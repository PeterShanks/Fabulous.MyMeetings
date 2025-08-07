using System.Reflection;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Configuration;

public static class Settings
{
    private static readonly Lazy<IReadOnlyCollection<Assembly>> AssembliesLazy = new(
        () => typeof(Settings)
            .Assembly
            .GetReferencedAssemblies()
            .Where(assemblyName => assemblyName.Name!.StartsWith("Fabulous.MyMeetings.Modules.Meetings") ||
                                   assemblyName.Name.StartsWith("Fabulous.MyMeetings.BuildingBlocks"))
            .Select(Assembly.Load)
            .Concat([typeof(Settings).Assembly])
            .ToList()
            .AsReadOnly());

    private static readonly Lazy<IReadOnlyCollection<Type>> AssemblyTypes =
        new(() => Assemblies.SelectMany(a => a.GetTypes()).ToList());

    public static IReadOnlyCollection<Assembly> Assemblies => AssembliesLazy.Value;

    public static IReadOnlyCollection<Type> AllTypes => AssemblyTypes.Value;
    public static Assembly? GetAssemblyOfType(this string typeName)
    {
        return Assemblies.SingleOrDefault(assembly => typeName.StartsWith(assembly.GetName().Name!));
    }

    /// <exception cref="ArgumentException">Might throw an error when throwOnError is true and type or assembly are not found.</exception>
    public static Type? GetTypeByName(this string typeName, bool ignoreCase = true, bool throwOnError = true)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(typeName);

        var assembly = typeName.GetAssemblyOfType();

        if (assembly == null)
        {
            if (throwOnError)
                throw new ArgumentException($"Couldn't find assembly for type {typeName}");

            return null;
        }

        return assembly.GetType(typeName, throwOnError, ignoreCase);
    }

    public static class PollyPolicies
    {
        public const string WaitAndRetry = nameof(WaitAndRetry);
    }
}