namespace Fabulous.MyMeetings.Modules.Administration.Application.Members.CreateMember;

public class CreateMemberCommand(
    Guid id,
    Guid memberId,
    string email,
    string firstName,
    string lastName,
    string name): InternalCommand<Guid>(id)
{
    public Guid MemberId { get; } = memberId;
    public string Email { get; } = email;
    public string FirstName { get; } = firstName;
    public string LastName { get; } = lastName;
    public string Name { get; } = name;
}