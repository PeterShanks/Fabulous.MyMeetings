using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations.Rules;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;

public class MeetingCommentingConfiguration: Entity, IAggregateRoot
{
    public MeetingCommentingConfigurationId Id { get; }
    public MeetingId MeetingId { get; }
    public bool IsCommentingEnabled { get; private set; }

    private MeetingCommentingConfiguration()
    {
        // Only for EF.
    }

    public static MeetingCommentingConfiguration Create(MeetingId meetingId)
    {
        return new MeetingCommentingConfiguration(meetingId);
    }

    private MeetingCommentingConfiguration(MeetingId meetingId)
    {
        Id = new MeetingCommentingConfigurationId(Guid.CreateVersion7());
        MeetingId = meetingId;
        IsCommentingEnabled = true;

        AddDomainEvent(new MeetingCommentingConfigurationCreatedDomainEvent(MeetingId, IsCommentingEnabled));
    }

    public void EnableCommenting(MemberId enablingMemberId, MeetingGroup meetingGroup)
    {
        CheckRule(new MeetingCommentingCanBeEnabledOnlyByGroupOrganizerRule(enablingMemberId, meetingGroup));

        if (!IsCommentingEnabled)
        {
            IsCommentingEnabled = true;
            AddDomainEvent(new MeetingCommentingEnabledDomainEvent(MeetingId));
        }
    }

    public void DisableCommenting(MemberId disablingMemberId, MeetingGroup meetingGroup)
    {
        CheckRule(new MeetingCommentingCanBeDisabledOnlyByGroupOrganizerRule(disablingMemberId, meetingGroup));
        if (IsCommentingEnabled)
        {
            IsCommentingEnabled = false;
            AddDomainEvent(new MeetingCommentingDisabledDomainEvent(MeetingId));
        }
    }
}