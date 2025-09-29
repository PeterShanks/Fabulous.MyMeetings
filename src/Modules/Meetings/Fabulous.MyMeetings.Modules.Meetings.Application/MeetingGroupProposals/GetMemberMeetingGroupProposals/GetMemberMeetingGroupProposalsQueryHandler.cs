using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMemberMeetingGroupProposals;

internal class GetMemberMeetingGroupProposalsQueryHandler(
    ISqlConnectionFactory sqlConnectionFactory,
    IMemberContext memberContext): IQueryHandler<GetMemberMeetingGroupProposalsQuery, IEnumerable<MeetingGroupProposalDto>>
{
    public Task<IEnumerable<MeetingGroupProposalDto>> Handle(GetMemberMeetingGroupProposalsQuery request, CancellationToken cancellationToken)
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
            WHERE [MeetingGroupProposal].[ProposalUserId] = @MemberId
            """;

        return connection.QueryAsync<MeetingGroupProposalDto>(sql, new
        {
            MemberId = memberContext.MemberId.Value
        });
    }
}