using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals;

public class GetAllMeetingGroupProposalsQuery(int? take, int? skip) : PagedQuery<MeetingGroupProposalDto>(take, skip);