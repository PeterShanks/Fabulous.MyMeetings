using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

internal class AttendeesLimitCannotBeChangedToSmallerThanActiveAttendeesRule(
    MeetingLimits meetingLimits,
    int allActiveAttendeesWithGuestsNumber)
    : IBusinessRule
{
    private readonly int? _attendeesLimit = meetingLimits.AttendeesLimit;

    public bool IsBroken() => _attendeesLimit < allActiveAttendeesWithGuestsNumber;

    public string Message => "Attendees limit cannot be change to smaller than active attendees number";
}