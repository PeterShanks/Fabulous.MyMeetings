namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes;

public class EditMeetingGroupGeneralAttributesCommand(
    Guid meetingGroupId,
    string name,
    string description,
    string locationCity,
    string locationCountry)
    : Command
{
    public string LocationCountry { get; } = locationCountry;

    internal Guid MeetingGroupId { get; } = meetingGroupId;

    internal string Name { get; } = name;

    internal string Description { get; } = description;

    internal string LocationCity { get; } = locationCity;
}