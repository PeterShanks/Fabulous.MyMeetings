namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

public class MeetingGroupProposalStatus: ValueObject
{
    public static MeetingGroupProposalStatus ToVerify => new(nameof(ToVerify));
    public static MeetingGroupProposalStatus Verified => new(nameof(Verified));
    internal static MeetingGroupProposalStatus Create(string value)
    {
        return new MeetingGroupProposalStatus(value);
    }

    public string Value { get; }
    private MeetingGroupProposalStatus(string value)
    {
        Value = value;
    }
}