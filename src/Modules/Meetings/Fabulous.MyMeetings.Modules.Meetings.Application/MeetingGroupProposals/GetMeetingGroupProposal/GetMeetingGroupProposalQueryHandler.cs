using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

public class GetMeetingGroupProposalQueryHandler(ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMeetingGroupProposalQuery, MeetingGroupProposalDto>
{
    public Task<MeetingGroupProposalDto> Handle(GetMeetingGroupProposalQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [MeetingGroupProposal].[Id],
                [MeetingGroupProposal].[Name],
                [MeetingGroupProposal].[Description],
                [MeetingGroupProposal].[LocationCity],
                [MeetingGroupProposal].[LocationCountryCode],
                [MeetingGroupProposal].[ProposalUserId],
                [MeetingGroupProposal].[ProposalDate],
                [MeetingGroupProposal].[StatusCode]
            FROM [Meetings].[v_MeetingGroupProposals] AS [MeetingGroupProposal]
            WHERE [MeetingGroupProposal].[Id] = @MeetingGroupProposalId
            """;

        return connection.QuerySingleAsync<MeetingGroupProposalDto>(sql, new
        {
            request.MeetingGroupProposalId
        });
    }
}