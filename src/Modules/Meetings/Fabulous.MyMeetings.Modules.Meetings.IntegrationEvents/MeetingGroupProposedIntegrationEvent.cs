using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.EventBus;

namespace Fabulous.MyMeetings.Modules.Meetings.IntegrationEvents;

public class MeetingGroupProposedIntegrationEvent(
    Guid id,
    DateTime occurredOn,
    Guid meetingGroupProposalId,
    string name,
    string description,
    string locationCity,
    string locationCountryCode,
    Guid proposalUserId,
    DateTime proposalDate) : IntegrationEvent(id, occurredOn)
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string LocationCity { get; } = locationCity;
    public string LocationCountryCode { get; } = locationCountryCode;
    public Guid ProposalUserId { get; } = proposalUserId;
    public DateTime ProposalDate { get; } = proposalDate;
}