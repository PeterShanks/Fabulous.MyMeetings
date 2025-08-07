using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings.Rules;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingLimits(int? attendeesLimit, int guestsLimit) : ValueObject
{
    public int? AttendeesLimit { get; } = attendeesLimit;
    public int GuestsLimit { get; } = guestsLimit;

    public static MeetingLimits Create(int? attendeesLimit, int guestsLimit)
    {
        CheckRule(new MeetingAttendeesLimitCannotBeNegativeRule(attendeesLimit));

        CheckRule(new MeetingGuestsLimitCannotBeNegativeRule(guestsLimit));

        CheckRule(new MeetingAttendeesLimitMustBeGreaterThanGuestsLimitRule(attendeesLimit, guestsLimit));

        return new MeetingLimits(attendeesLimit, guestsLimit);
    }

}