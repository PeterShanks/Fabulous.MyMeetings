using Fabulous.MyMeetings.Modules.Administration.Application.Members.CreateMember;
using Fabulous.MyMeetings.Modules.UserRegistrations.IntegrationEvents;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Administration.Application.Members;

public class NewUserRegisteredIntegrationEventHandler(ICommandsScheduler commandsScheduler): INotificationHandler<NewUserRegisteredIntegrationEvent>
{
    public Task Handle(NewUserRegisteredIntegrationEvent notification, CancellationToken cancellationToken)
    {
        return commandsScheduler.EnqueueAsync(
            new CreateMemberCommand(
                Guid.CreateVersion7(),
                notification.UserId,
                notification.Email,
                notification.FirstName,
                notification.LastName,
                notification.Name));
    }
}