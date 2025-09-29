using Fabulous.MyMeetings.Modules.Administration.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Domain.Members;

public class MemberRepository(AdministrationContext context): IMemberRepository
{
    public Task AddAsync(Member member)
    {
        return context.Members.AddAsync(member).AsTask();
    }

    public Task<Member?> GetByIdAsync(MemberId memberId)
    {
        return context.Members.FindAsync(memberId).AsTask();
    }
}