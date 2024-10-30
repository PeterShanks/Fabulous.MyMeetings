using Serilog.Core;
using Serilog.Events;

namespace Fabulous.MyMeetings.Api
{
    public class ModuleEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddPropertyIfAbsent(logEvent.Properties.TryGetValue("Module", out var module)
                ? propertyFactory.CreateProperty("Module", module)
                : propertyFactory.CreateProperty("Module", "SYSTEM"));
        }
    }
}
