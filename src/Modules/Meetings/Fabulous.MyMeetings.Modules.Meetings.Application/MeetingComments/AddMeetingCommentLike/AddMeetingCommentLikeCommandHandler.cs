using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;

public class AddMeetingCommentLikeCommandHandler(
    IMeetingCommentRepository meetingCommentRepository,
    IMeetingMemberCommentLikesRepository meetingMemberCommentLikesRepository,
    IMemberContext memberContext,
    ISqlConnectionFactory sqlConnectionFactory): ICommandHandler<AddMeetingCommentLikeCommand>
{
    public async Task Handle(AddMeetingCommentLikeCommand request, CancellationToken cancellationToken)
    {
        var meetingComment =
            await meetingCommentRepository.GetByIdAsync(new MeetingCommentId(request.MeetingCommentId));

        if (meetingComment is null)
            throw new InvalidCommandException(["To add like the comment must exist."]);

        var connection = sqlConnectionFactory.GetOpenConnection();
        const string sql = $"""
                            SELECT
                                [MeetingGroupMember].[MeetingGroupId],
                                [MeetingGroupMember].[MemberId]
                            FROM [Meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember]
                                 INNER JOIN [Meetings].[Meetings] AS [Meeting]
                                         ON [Meeting].[MeetingGroupId] = [MeetingGroupMember].[MeetingGroupId]
                            WHERE [MeetingGroupMember].[MemberId] = @MemberId
                                  AND [Meeting].[Id] = @MeetingId
                            """;

        var likerMeetingGroupMemberData = await connection.QuerySingleAsync<MeetingGroupMemberResponse>(
            sql,
            new
            {
                MemberId = memberContext.MemberId.Value,
                MeetingId = meetingComment.MeetingId.Value
            });

        var meetingMemberCommentLikesCount = await meetingMemberCommentLikesRepository.CountMemberCommentLikesAsync(
            memberContext.MemberId,
            new MeetingCommentId(request.MeetingCommentId));

        var like = meetingComment.Like(
            memberContext.MemberId,
            new MeetingGroupMemberData(
                new MeetingGroupId(likerMeetingGroupMemberData.MeetingGroupId),
                new MemberId(likerMeetingGroupMemberData.MemberId)),
            meetingMemberCommentLikesCount);

        await meetingMemberCommentLikesRepository.AddAsync(like);
    }

    private class MeetingGroupMemberResponse
    {
        public Guid MeetingGroupId { get; set; }

        public Guid MemberId { get; set; }
    }
}