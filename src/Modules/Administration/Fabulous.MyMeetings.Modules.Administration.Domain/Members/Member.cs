using Fabulous.MyMeetings.Modules.Administration.Domain.Members.Events;

namespace Fabulous.MyMeetings.Modules.Administration.Domain.Members;

public class Member: Entity, IAggregateRoot
{
    public MemberId Id { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Name { get; }
    public DateTime CreatedDate { get; }

    private Member()
    {
        // FOR EF
    }

    private Member(Guid id, string email, string firstName, string lastName, string name)
    {
        Id = new MemberId(id);
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Name = name;
        CreatedDate = DateTime.UtcNow;

        AddDomainEvent(new MemberCreatedDomainEvent(Id));
    }

    public static Member Create(Guid id, string email, string firstName, string lastName, string name) =>
        new(id, email, firstName, lastName, name);
}