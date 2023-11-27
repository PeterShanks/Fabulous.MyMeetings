using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Emails
{
    internal class GetAllEmailsQueryHandler: IQueryHandler<GetAllEmailsQuery, List<EmailDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetAllEmailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<EmailDto>> Handle(GetAllEmailsQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

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
}
