using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Events;

public class MeetingAttendeeAddedDomainEvent(
    MeetingId meetingId,
    MemberId attendeeId,
    DateTime rsvpDate,
    string role,
    int guestsNumber,
    decimal? feeValue,
    string? feeCurrency):DomainEvent
{
    public MeetingId MeetingId { get; } = meetingId;
    public MemberId AttendeeId { get; } = attendeeId;
    public DateTime RsvpDate { get; } = rsvpDate;
    public string Role { get; } = role;
    public int GuestsNumber { get; } = guestsNumber;
    public decimal? FeeValue { get; } = feeValue;
    public string? FeeCurrency { get; } = feeCurrency;
}