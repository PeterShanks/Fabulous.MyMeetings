using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

public class AttendeeCanBeAddedOnlyInRsvpTermRule(Term rsvpTerm) : IBusinessRule
{
    public bool IsBroken() => !rsvpTerm.IsInTerm(DateTime.UtcNow);

    public string Message => "Attendee can be added only in RSVP term";
}