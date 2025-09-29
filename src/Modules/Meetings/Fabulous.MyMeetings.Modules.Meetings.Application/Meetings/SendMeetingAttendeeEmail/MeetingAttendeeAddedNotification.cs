using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SendMeetingAttendeeEmail;

public class MeetingAttendeeAddedNotification(MeetingAttendeeAddedDomainEvent domainEvent, Guid id) 
    : DomainEventNotification<MeetingAttendeeAddedDomainEvent>(domainEvent, id);