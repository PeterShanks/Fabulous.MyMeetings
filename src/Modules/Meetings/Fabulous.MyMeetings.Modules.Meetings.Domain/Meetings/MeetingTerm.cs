using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class MeetingTerm: ValueObject
{
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }

    public static MeetingTerm CreateNewBetweenDates(DateTime startDate, DateTime endDate)
    {
        return new MeetingTerm(startDate, endDate);
    }

    private MeetingTerm(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    internal bool IsAfterStart() => DateTime.UtcNow > StartDate;
}