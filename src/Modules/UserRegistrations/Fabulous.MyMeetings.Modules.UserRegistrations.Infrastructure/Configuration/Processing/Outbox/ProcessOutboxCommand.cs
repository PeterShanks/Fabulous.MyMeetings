using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Processing.Outbox;

public class ProcessOutboxCommand : Command, IRecurringCommand;