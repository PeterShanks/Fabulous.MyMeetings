namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.Users.Events;

public class UserCreatedDomainEvent(UserId id) : DomainEvent
{
    public new UserId Id { get; } = id;
}