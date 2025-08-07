using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;
using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Rules;
using Fabulous.MyMeetings.Modules.Administration.Domain.Users;

namespace Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;

public class MeetingGroupProposal: Entity, IAggregateRoot
{
    public MeetingGroupProposalId Id { get; }
    public string Name { get; }
    public string Description { get; }
    public MeetingGroupLocation Location { get; }
    public DateTime ProposalDate { get; }
    public UserId ProposalUserId { get; }
    public MeetingGroupProposalStatus Status { get; private set; }
    public MeetingGroupProposalDecision Decision { get; private set; }

    private MeetingGroupProposal()
    {
        Decision = MeetingGroupProposalDecision.NoDecision;
    }

    private MeetingGroupProposal(MeetingGroupProposalId id, string name, string description, MeetingGroupLocation location, DateTime proposalDate, UserId proposalUserId)
    {
        Id = id;
        Name = name;
        Description = description;
        Location = location;
        ProposalDate = proposalDate;
        ProposalUserId = proposalUserId;
        Status = MeetingGroupProposalStatus.ToVerify;
        Decision = MeetingGroupProposalDecision.NoDecision;
    }

    public void Accept(UserId userId)
    {
        CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(Decision));

        Decision = MeetingGroupProposalDecision.AcceptDecision(DateTime.UtcNow, userId);
        Status = Decision.GetStatusForDecision();

        AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(Id));
    }

    public void Reject(UserId userId, string rejectReason)
    {
        CheckRule(new MeetingGroupProposalCanBeVerifiedOnceRule(Decision));
        CheckRule(new MeetingGroupProposalRejectionMustHaveAReasonRule(rejectReason));

        Decision = MeetingGroupProposalDecision.RejectDecision(DateTime.UtcNow, userId, rejectReason);
        Status = Decision.GetStatusForDecision();

        AddDomainEvent(new MeetingGroupProposalRejectedDomainEvent(Id));
    }

    public static MeetingGroupProposal CreateToVerify(
        Guid meetingGroupProposalId,
        string name,
        string description,
        MeetingGroupLocation location,
        UserId proposalUserId,
        DateTime proposalDate)
        => new MeetingGroupProposal(new MeetingGroupProposalId(meetingGroupProposalId), name, description, location,
            proposalDate, proposalUserId);
}