using Fabulous.MyMeetings.Modules.Administration.Domain.Users;

namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

public class MeetingGroupProposalDecision: ValueObject
{
    private const string Accept = "Accept";
    private const string Reject = "Reject";

    private MeetingGroupProposalDecision(DateTime? date, UserId? userId, string? code, string? rejectReason)
    {
        Date = date;
        UserId = userId;
        Code = code;
        RejectReason = rejectReason;
    }

    public DateTime? Date { get; }
    public UserId? UserId { get; }
    public string? Code { get; }
    public string? RejectReason { get; }

    internal static MeetingGroupProposalDecision NoDecision => new(null, null, null, null);

    private bool IsAccepted => Code == Accept;
    private bool IsRejected => Code == Reject;

    internal static MeetingGroupProposalDecision AcceptDecision(DateTime date, UserId userId) =>
        new(date, userId, Accept, null);

    internal static MeetingGroupProposalDecision RejectDecision(DateTime date, UserId userId, string rejectReason) =>
        new(date, userId, Reject, rejectReason);

    internal MeetingGroupProposalStatus GetStatusForDecision()
    {
        if (IsAccepted)
            return MeetingGroupProposalStatus.Verified;

        if (IsRejected)
            return MeetingGroupProposalStatus.Create("Rejected");

        return MeetingGroupProposalStatus.ToVerify;
    }
}