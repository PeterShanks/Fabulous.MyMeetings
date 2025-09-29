using Fabulous.MyMeetings.Modules.Administration.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Administration.Application.Members.CreateMember;

public class CreateMemberCommandHandler(IMemberRepository memberRepository): ICommandHandler<CreateMemberCommand, Guid>
{
    public async Task<Guid> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = Member.Create(
            request.MemberId,
            request.Email,
            request.FirstName,
            request.LastName,
            request.Name);

        await memberRepository.AddAsync(member);

        return member.Id;
    }
}