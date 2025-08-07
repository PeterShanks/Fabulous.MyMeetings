using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Members.CreateMember;

public class MemberCreatedNotification(MeetingCreatedDomainEvent domainEvent, Guid id) 
    : DomainEventNotification<MeetingCreatedDomainEvent>(domainEvent, id);