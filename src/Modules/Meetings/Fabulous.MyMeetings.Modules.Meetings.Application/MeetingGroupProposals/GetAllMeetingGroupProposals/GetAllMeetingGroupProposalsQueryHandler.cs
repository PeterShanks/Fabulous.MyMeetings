using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals;

public class GetAllMeetingGroupProposalsQueryHandler(ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetAllMeetingGroupProposalsQuery, IEnumerable<MeetingGroupProposalDto>>
{
    public Task<IEnumerable<MeetingGroupProposalDto>> Handle(GetAllMeetingGroupProposalsQuery request, CancellationToken cancellationToken)
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
            ORDER BY [MeetingGroupProposal].[Name]
            OFFSET @Skip ROWS
            FETCH NEXT @Take ROWS ONLY
            """;

        return connection.QueryAsync<MeetingGroupProposalDto>(
            sql,
            new
            {
                Skip = request.Skip,
                Take = request.Take
            });
    }
}