using System.Reflection;
using System.Text.Json;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure
{
    public static class Settings
    {
        private static readonly Lazy<JsonSerializerOptions> LazyJsonSerializerOptions =
            new Lazy<JsonSerializerOptions>(() => new JsonSerializerOptions(JsonSerializerDefaults.Web));

        public static JsonSerializerOptions JsonSerializerOptionsInstance => LazyJsonSerializerOptions.Value;

        private static readonly Lazy<IReadOnlyCollection<Assembly>> AssembliesLazy = new(
            () => typeof(Settings)
                    .Assembly
                    .GetReferencedAssemblies()
                    .Select(assemblyName => Assembly.Load(assemblyName))
                    .Concat(new[] {typeof(Settings).Assembly })
                    .ToList()
                    .AsReadOnly());

        public static IReadOnlyCollection<Assembly> Assemblies => AssembliesLazy.Value;

        public static Assembly? GetAssemblyOfType(this string typeName) =>
            Assemblies.SingleOrDefault(assembly => typeName.StartsWith(assembly.GetName().Name!));

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

            return assembly.GetType(typeName, throwOnError: throwOnError, ignoreCase: ignoreCase);
        }

        private static readonly Lazy<IReadOnlyCollection<Type>> AssemblyTypes =
            new(() => Assemblies.SelectMany(a => a.GetTypes()).ToList());

        public static IReadOnlyCollection<Type> AllTypes => AssemblyTypes.Value;

        public static class PollyPolicies
        {
            public const string WaitAndRetry = nameof(WaitAndRetry);
        }
    }
}
