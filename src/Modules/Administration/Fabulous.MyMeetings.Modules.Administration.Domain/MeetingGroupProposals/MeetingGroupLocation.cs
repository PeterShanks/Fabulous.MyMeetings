namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

public class MeetingGroupLocation: ValueObject
{
    public string City { get; }
    public string CountryCode { get; }

    private MeetingGroupLocation(string city, string countryCode)
    {
        City = city;
        CountryCode = countryCode;
    }

    public static MeetingGroupLocation Create(string city, string countryCode)
    {
        return new MeetingGroupLocation(city, countryCode);
    }
}