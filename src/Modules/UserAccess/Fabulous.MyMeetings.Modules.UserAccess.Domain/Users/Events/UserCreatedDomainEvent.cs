namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEvent
    {
        public UserCreatedDomainEvent(UserId id)
        {
            Id = id;
        }

        public new UserId Id { get; }
    }
}
