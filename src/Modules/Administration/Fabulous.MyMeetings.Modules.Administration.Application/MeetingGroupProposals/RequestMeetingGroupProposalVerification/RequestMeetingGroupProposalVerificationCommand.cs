namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.RequestMeetingGroupProposalVerification;

public class RequestMeetingGroupProposalVerificationCommand(
    Guid id,
    Guid meetingGroupProposalId,
    string name,
    string description,
    string locationCity,
    string locationCountryCode,
    Guid proposalUserId,
    DateTime proposalDate): InternalCommand<Guid>(id)
{
    public Guid MeetingGroupProposalId { get; } = meetingGroupProposalId;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string LocationCity { get; } = locationCity;
    public string LocationCountryCode { get; } = locationCountryCode;
    public Guid ProposalUserId { get; } = proposalUserId;
    public DateTime ProposalDate { get; } = proposalDate;
}