using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;

public class Term: ValueObject
{
    public static Term NoTerm => new(null, null);
    public DateTime? StartDate { get; }
    public DateTime? EndDate { get; }

    public static Term CreateNewBetweenDates(DateTime? startDate, DateTime? endDate)
    {
        return new Term(startDate, endDate);
    }

    private Term(DateTime? startDate, DateTime? endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    internal bool IsInTerm(DateTime date)
    {
        if (date < StartDate)
        {
            return false;
        }
        if (date > EndDate)
        {
            return false;
        }
        return true;
    }
}