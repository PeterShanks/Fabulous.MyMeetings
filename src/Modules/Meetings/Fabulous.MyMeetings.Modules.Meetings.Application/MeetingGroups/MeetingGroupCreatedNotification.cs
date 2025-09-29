using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups;

public class MeetingGroupCreatedNotification(MeetingGroupCreatedDomainEvent domainEvent, Guid id) : DomainEventNotification<MeetingGroupCreatedDomainEvent>(domainEvent, id);