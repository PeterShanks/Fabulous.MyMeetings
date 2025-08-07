using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Rules;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;

public class MeetingGroupProposal: Entity, IAggregateRoot
{
    public MeetingGroupProposalId Id { get; }
    public string Name { get; }
    public string Description { get; }
    public MeetingGroupLocation Location { get; }
    public DateTime ProposalDate { get; }
    public MemberId ProposalUserId { get; }
    public MeetingGroupProposalStatus Status { get; private set; }

    public static MeetingGroupProposal ProposeNew(
        string name, string description,
        MeetingGroupLocation location, MemberId proposalUserId)
        => new MeetingGroupProposal(name, description, location, proposalUserId);

    private MeetingGroupProposal()
    {
        // Only for EF.
    }

    private MeetingGroupProposal(string name, string description, MeetingGroupLocation location,
        MemberId proposalUserId)
    {
        Id = new MeetingGroupProposalId(Guid.CreateVersion7());
        Name = name;
        Description = description;
        Location = location;
        ProposalDate = DateTime.UtcNow;
        ProposalUserId = proposalUserId;
        Status = MeetingGroupProposalStatus.InVerification;

        AddDomainEvent(new MeetingGroupProposedDomainEvent(Id, Name, Description, Location.City,
            Location.CountryCode, proposalUserId, ProposalDate));
    }

    public MeetingGroup CreateMeetingGroup()
    {
        return MeetingGroup.CreateBasedOnProposal(Id, Name, Description, Location, ProposalUserId);
    }

    public void Accept()
    {
        CheckRule(new MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(Status));

        Status = MeetingGroupProposalStatus.Accepted;

        AddDomainEvent(new MeetingGroupProposalAcceptedDomainEvent(Id));
    }

}