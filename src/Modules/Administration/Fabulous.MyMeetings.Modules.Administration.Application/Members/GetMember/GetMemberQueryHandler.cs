using Fabulous.MyMeetings.BuildingBlocks.Application.Data;

namespace Fabulous.MyMeetings.Modules.Administration.Application.Members.GetMember;

internal class GetMemberQueryHandler(ISqlConnectionFactory sqlConnectionFactory): IQueryHandler<GetMemberQuery, MemberDto>
{
    public Task<MemberDto> Handle(GetMemberQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                [Member].[Id],
                [Member].[Name],
                [Member].[Email],
                [Member].[FirstName],
                [Member].[LastName]
            """;

        return connection.QuerySingleAsync<MemberDto>(sql, new { request.MemberId });
    }
}