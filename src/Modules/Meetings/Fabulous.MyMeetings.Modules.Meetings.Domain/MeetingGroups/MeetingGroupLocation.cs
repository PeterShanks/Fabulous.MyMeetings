using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public class MeetingGroupLocation: ValueObject
{
    public string City { get; }
    public string CountryCode { get; }

    private MeetingGroupLocation(string city, string countryCode)
    {
        City = city;
        CountryCode = countryCode;
    }

    public static MeetingGroupLocation CreateNew(string city, string countryCode)
    {
        return new MeetingGroupLocation(city, countryCode);
    }
}