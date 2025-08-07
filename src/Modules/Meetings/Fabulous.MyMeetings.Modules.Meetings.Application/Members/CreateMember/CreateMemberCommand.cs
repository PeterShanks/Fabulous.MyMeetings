namespace Fabulous.MyMeetings.Modules.Meetings.Application.Members.CreateMember;

public class CreateMemberCommand(
    Guid id,
    Guid memberId,
    string email,
    string firstName,
    string lastName,
    string name): InternalCommand(id)
{
    public Guid MemberId { get; } = memberId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Name { get; } = name;
}