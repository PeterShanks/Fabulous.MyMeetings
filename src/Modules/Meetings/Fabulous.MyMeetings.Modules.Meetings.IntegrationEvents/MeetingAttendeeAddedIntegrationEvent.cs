using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;

public class MeetingAttendeeAddedIntegrationEvent(
    Guid id,
    DateTime occurredOn,
    Guid meetingId,
    Guid attendeeId,
    decimal? feeValue, 
    string? feeCurrency): IntegrationEvent(id, occurredOn)
{
    public Guid MeetingId { get; } = meetingId;
    public Guid AttendeeId { get; } = attendeeId;
    public decimal? FeeValue { get; } = feeValue;
    public string? FeeCurrency { get; } = feeCurrency;
}