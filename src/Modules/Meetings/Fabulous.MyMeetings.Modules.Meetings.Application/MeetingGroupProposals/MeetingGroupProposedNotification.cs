using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals;

public class MeetingGroupProposedNotification(MeetingGroupProposedDomainEvent domainEvent, Guid id)
    : DomainEventNotification<MeetingGroupProposedDomainEvent>(domainEvent, id);