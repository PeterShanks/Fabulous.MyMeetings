using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

public class Member: Entity, IAggregateRoot
{
    public MemberId Id { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Name { get; }
    public DateTime CreatedAt { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Member(){}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Member(Guid id, string email, string firstName, string lastName, string name)
    {
        Id = new MemberId(id);
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = name;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new MemberCreatedDomainEvent(Id));
    }

    public static Member Create(Guid id, string email, string firstName, string lastName, string name)
    {
        return new Member(id, email, firstName, lastName, name);
    }
}