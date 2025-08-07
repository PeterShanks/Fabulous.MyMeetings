using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposal;

public class GetMeetingGroupProposalQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingGroupProposalQuery, MeetingGroupProposalDto>
{
    public Task<MeetingGroupProposalDto> Handle(GetMeetingGroupProposalQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

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
            WHERE [MeetingGroupProposal].[Id] = @MeetingGroupProposalId"
            """;

        return connection.QuerySingleAsync<MeetingGroupProposalDto>(
            sql,
            new { request.MeetingGroupProposalId });
    }
}