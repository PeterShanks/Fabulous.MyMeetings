using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes.Events;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments;

public class MeetingCommentLikedNotification(MeetingCommentLikedDomainEvent domainEvent, Guid id) :DomainEventNotification<MeetingCommentLikedDomainEvent>(domainEvent, id);