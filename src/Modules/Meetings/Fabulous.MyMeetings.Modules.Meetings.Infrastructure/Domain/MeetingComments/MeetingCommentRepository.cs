using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingComments;

public class MeetingCommentRepository(MeetingsContext context): IMeetingCommentRepository
{
    public Task AddAsync(MeetingComment meetingComment)
        => context.MeetingComments.AddAsync(meetingComment).AsTask();

    public Task<MeetingComment?> GetByIdAsync(MeetingCommentId meetingCommentId)
        => context.MeetingComments.FindAsync(meetingCommentId).AsTask();
}