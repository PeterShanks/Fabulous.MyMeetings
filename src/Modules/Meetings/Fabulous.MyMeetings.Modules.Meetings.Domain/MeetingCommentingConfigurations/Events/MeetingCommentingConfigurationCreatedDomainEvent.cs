using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;

public class MeetingCommentingConfigurationCreatedDomainEvent(MeetingId meetingId, bool isEnabled) : DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;

    public bool IsEnabled { get; } = isEnabled;
}