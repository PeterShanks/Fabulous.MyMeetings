using Fabulous.MyMeetings.BuildingBlocks.Domain;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

public class MeetingGroupProposalStatus: ValueObject
{
    public string Value { get; }

    internal static MeetingGroupProposalStatus InVerification => new MeetingGroupProposalStatus(nameof(InVerification));
    internal static MeetingGroupProposalStatus Accepted => new MeetingGroupProposalStatus(nameof(Accepted));

    internal bool IsAccepted => Value == nameof(Accepted);

    private MeetingGroupProposalStatus(string value)
    {
        Value = value;
    }
}