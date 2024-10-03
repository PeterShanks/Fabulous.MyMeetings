using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Emails;

internal class GetAllEmailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetAllEmailsQuery, List<EmailDto>>
{
    public async Task<List<EmailDto>> Handle(GetAllEmailsQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        var emailsEnumerable = await connection.QueryAsync<EmailDto>(
            """
            SELECT
                Id,
                From,
                To,
                Subject,
                Content,
                Date
            FROM app.Emails
            ORDER BY Date DESC
            """);

        return emailsEnumerable.AsList();
    }
}