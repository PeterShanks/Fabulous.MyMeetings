using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.SetMeetingGroupExpirationDate;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Policies;
using MediatR;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MemberSubscriptions;

public class MemberSubscriptionExpirationDateChangedNotificationHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    ICommandsScheduler commandsScheduler): INotificationHandler<MemberSubscriptionExpirationDateChangedNotification>
{
    public async Task Handle(MemberSubscriptionExpirationDateChangedNotification notification, CancellationToken cancellationToken)
    {
        const string sql = 
            $"""
             SELECT
                 [MeetingGroupMember].MeetingGroupId AS {nameof(MeetingGroupMemberResponse.MeetingGroupId)},
                 [MeetingGroupMember].RoleCode AS {nameof(MeetingGroupMemberResponse.RoleCode)}
             FROM [Meetings].[v_MeetingGroupMembers] AS [MeetingGroupMember]
             WHERE [MeetingGroupMember].MemberId = @MemberId      
             """;

        var connection = sqlConnectionFactory.GetOpenConnection();

        var meetingGroupMembers = await connection.QueryAsync<MeetingGroupMemberResponse>(
            sql,
            new
            {
                MemberId = notification.DomainEvent.MemberId.Value
            });


        var meetingGroups = meetingGroupMembers
            .Select(x => 
                new MeetingGroupMemberData(
                    new MeetingGroupId(x.MeetingGroupId),
                    MeetingGroupMemberRole.Of(x.RoleCode)))
            .ToList();

        var meetingGroupsCoveredByMemberSubscription = meetingGroups
            .Where(x => x.Role == MeetingGroupMemberRole.Organizer)
            .Select(x => x.MeetingGroupId)
            .ToList();

        foreach (var meetingGroupId in meetingGroupsCoveredByMemberSubscription)
        {
            await commandsScheduler.EnqueueAsync(new SetMeetingGroupExpirationDateCommand(
                Guid.CreateVersion7(),
                meetingGroupId,
                notification.DomainEvent.ExpirationDate));
        }
    }

    private class MeetingGroupMemberResponse
    {
        public Guid MeetingGroupId { get; set; }

        public string RoleCode { get; set; }
    }
}