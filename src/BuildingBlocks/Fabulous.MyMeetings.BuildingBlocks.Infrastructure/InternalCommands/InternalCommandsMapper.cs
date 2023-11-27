namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommandsMapper: IInternalCommandsMapper
    {
        private readonly BiDictionary<string, Type> _internalCommandMap;

        public InternalCommandsMapper(BiDictionary<string, Type> internalCommandMap)
        {
            _internalCommandMap = internalCommandMap;
        }

        public string? GetName(Type type)
        {
            return _internalCommandMap.TryGetBySecond(type, out var name) ? name : null;
        }

        public Type? GetType(string name)
        {
            return _internalCommandMap.TryGetByFirst(name, out var type) ? type : null;
        }
    }
}
