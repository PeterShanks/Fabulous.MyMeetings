using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members;

public class MemberRepository(MeetingsContext context): IMemberRepository
{
    public Task AddAsync(Member member)
        => context.Members.AddAsync(member).AsTask();

    public Task<Member?> GetByIdAsync(MemberId memberId)
        => context.Members
            .FindAsync(memberId)
            .AsTask();
}