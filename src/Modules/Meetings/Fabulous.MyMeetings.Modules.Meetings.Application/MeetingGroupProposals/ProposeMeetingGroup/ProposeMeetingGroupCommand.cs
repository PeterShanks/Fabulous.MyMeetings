namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;

public class ProposeMeetingGroupCommand(
    string name,
    string description,
    string locationCity,
    string locationCountryCode)
    : Command<Guid>
{
    public string Name { get; } = name;

    public string Description { get; } = description;

    public string LocationCity { get; } = locationCity;

    public string LocationCountryCode { get; } = locationCountryCode;
}