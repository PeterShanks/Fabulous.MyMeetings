using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments;

public class MeetingCommentUnlikedNotification(MeetingCommentUnlikedDomainEvent domainEvent, Guid id) : DomainEventNotification<MeetingCommentUnlikedDomainEvent>(domainEvent, id);