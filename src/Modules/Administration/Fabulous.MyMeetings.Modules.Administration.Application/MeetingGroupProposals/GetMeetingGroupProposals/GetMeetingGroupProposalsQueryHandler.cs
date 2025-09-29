using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals;

public class GetMeetingGroupProposalsQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingGroupProposalsQuery, IEnumerable<MeetingGroupProposalDto>>
{
    public Task<IEnumerable<MeetingGroupProposalDto>> Handle(GetMeetingGroupProposalsQuery request, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
                [MeetingGroupProposal].[Id],
                [MeetingGroupProposal].[Name],
                [MeetingGroupProposal].[ProposalUserId],
                [MeetingGroupProposal].[LocationCity],
                [MeetingGroupProposal].[LocationCountryCode],
                [MeetingGroupProposal].[Description],
                [MeetingGroupProposal].[ProposalDate],
                [MeetingGroupProposal].[StatusCode],
                [MeetingGroupProposal].[DecisionDate],
                [MeetingGroupProposal].[DecisionUserId],
                [MeetingGroupProposal].[DecisionCode],
                [MeetingGroupProposal].[DecisionRejectReason]
            FROM [Administration].[v_MeetingGroupProposals] as [MeetingGroupProposal]
            """;

        var connection = sqlConnectionFactory.GetOpenConnection();

        return connection.QueryAsync<MeetingGroupProposalDto>(sql);
    }
}