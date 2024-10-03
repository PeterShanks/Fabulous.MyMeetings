namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;

public class InternalCommandsMapper(BiDictionary<string, Type> internalCommandMap) : IInternalCommandsMapper
{
    public string? GetName(Type type)
    {
        return internalCommandMap.TryGetBySecond(type, out var name) ? name : null;
    }

    public Type? GetType(string name)
    {
        return internalCommandMap.TryGetByFirst(name, out var type) ? type : null;
    }
}