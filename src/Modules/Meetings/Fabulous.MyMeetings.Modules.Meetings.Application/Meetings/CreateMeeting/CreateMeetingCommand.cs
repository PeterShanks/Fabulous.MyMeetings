namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting;

public class CreateMeetingCommand(
    Guid meetingGroupId,
    string title,
    DateTime termStartDate,
    DateTime termEndDate,
    string description,
    string meetingLocationName,
    string meetingLocationAddress,
    string meetingLocationPostalCode,
    string meetingLocationCity,
    int? attendeesLimit,
    int guestsLimit,
    DateTime? rsvpTermStartDate,
    DateTime? rsvpTermEndDate,
    decimal? eventFeeValue,
    string eventFeeCurrency,
    List<Guid> hostMemberIds) : Command<Guid>
{
    public Guid MeetingGroupId { get; } = meetingGroupId;
    public string Title { get; } = title;
    public DateTime TermStartDate { get; } = termStartDate;
    public DateTime TermEndDate { get; } = termEndDate;
    public string Description { get; } = description;
    public string MeetingLocationName { get; } = meetingLocationName;
    public string MeetingLocationAddress { get; } = meetingLocationAddress;
    public string MeetingLocationPostalCode { get; } = meetingLocationPostalCode;
    public string MeetingLocationCity { get; } = meetingLocationCity;
    public int? AttendeesLimit { get; } = attendeesLimit;
    public int GuestsLimit { get; } = guestsLimit;
    public DateTime? RsvpTermStartDate { get; } = rsvpTermStartDate;
    public DateTime? RsvpTermEndDate { get; } = rsvpTermEndDate;
    public decimal? EventFeeValue { get; } = eventFeeValue;
    public string EventFeeCurrency { get; } = eventFeeCurrency;
    public List<Guid> HostMemberIds { get; } = hostMemberIds;
}