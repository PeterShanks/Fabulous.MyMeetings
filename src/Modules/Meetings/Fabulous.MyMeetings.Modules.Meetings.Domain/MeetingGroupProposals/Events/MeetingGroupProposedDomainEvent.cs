using Fabulous.MyMeetings.BuildingBlocks.Domain;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals.Events;

public class MeetingGroupProposedDomainEvent(
    MeetingGroupProposalId meetingGroupProposalId,
    string name,
    string description,
    string locationCity,
    string locationCountryCode,
    MemberId proposalUserId,
    DateTime proposalDate)
    : DomainEvent
{
    public MeetingGroupProposalId MeetingGroupProposalId { get; } = meetingGroupProposalId;

    public string Name { get; } = name;

    public string Description { get; } = description;

    public string LocationCity { get; } = locationCity;

    public string LocationCountryCode { get; } = locationCountryCode;

    public MemberId ProposalUserId { get; } = proposalUserId;

    public DateTime ProposalDate { get; } = proposalDate;
}