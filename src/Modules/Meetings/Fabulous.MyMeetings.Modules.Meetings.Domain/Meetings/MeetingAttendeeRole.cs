using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingAttendeeRole: ValueObject
{
    public static MeetingAttendeeRole Host => new MeetingAttendeeRole("Host");
    public static MeetingAttendeeRole Attendee => new MeetingAttendeeRole("Attendee");

    public string Value { get; }

    private MeetingAttendeeRole(string value)
    {
        Value = value;
    }
}