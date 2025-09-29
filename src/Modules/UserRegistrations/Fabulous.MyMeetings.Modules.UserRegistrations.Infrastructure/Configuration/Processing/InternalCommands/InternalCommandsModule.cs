using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing.InternalCommands;

public static class InternalCommandsModule
{
    public static IServiceCollection AddInternalCommandsModule(
        this IServiceCollection services,
        BiDictionary<string, Type> internalCommandsMap)
    {
        CheckMappings(internalCommandsMap);

        return services.AddSingleton<IInternalCommandsMapper>(new InternalCommandsMapper(internalCommandsMap));
    }

    private static void CheckMappings(BiDictionary<string, Type> internalCommandsMap)
    {
        var internalCommands = Assemblies
            .SelectMany(a => a.GetTypes())
            .Where(x => x.BaseType != null &&
                        (
                            (x.BaseType.IsGenericType &&
                             x.BaseType.GetGenericTypeDefinition() == typeof(InternalCommand<>)) ||
                            x.BaseType == typeof(InternalCommand)))
            .ToList();

        List<Type> notMappedInternalCommands = [];
        foreach (var internalCommand in internalCommands)
            if(!internalCommandsMap.TryGetBySecond(internalCommand, out _))
                notMappedInternalCommands.Add(internalCommand);

        if (notMappedInternalCommands.Any())
            throw new ApplicationException($"Internal Commands {notMappedInternalCommands.Select(x => x.FullName).Aggregate((x, y) => x + "," + y)} not mapped");
    }

}