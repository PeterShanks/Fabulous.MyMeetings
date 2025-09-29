using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes;

public class MeetingMemberCommentLikesRepository(MeetingsContext context) : IMeetingMemberCommentLikesRepository
{
    public Task AddAsync(MeetingMemberCommentLike meetingMemberCommentLike)
        => context
            .MeetingMemberCommentLikes
            .AddAsync(meetingMemberCommentLike)
            .AsTask();

    public Task<MeetingMemberCommentLike?> GetAsync(MemberId memberId, MeetingCommentId meetingCommentId)
        => context
            .MeetingMemberCommentLikes
            .FindAsync(memberId, meetingCommentId)
            .AsTask();

    public Task<int> CountMemberCommentLikesAsync(MemberId memberId, MeetingCommentId meetingCommentId)
        => context
            .MeetingMemberCommentLikes
            .CountAsync(like => like.MemberId == memberId && like.MeetingCommentId == meetingCommentId);

    public void Remove(MeetingMemberCommentLike meetingMemberCommentLike)
        => context
            .MeetingMemberCommentLikes
            .Remove(meetingMemberCommentLike);
}