using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Members.CreateMember;

public class CreateMemberCommandHandler(
    IMemberRepository memberRepository): ICommandHandler<CreateMemberCommand>
{
    public Task Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = Member.Create(
            request.MemberId,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Name);

        return memberRepository.AddAsync(member);
    }
}