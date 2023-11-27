using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser
{
    internal class GetUserQueryHandler: IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                """
                SELECT
                    Id,
                    IsActive,
                    Login,
                    Email,
                    Name
                    FROM Users.v_Users
                    WHERE Id = @UserId
                """;

            return connection.QuerySingleAsync<UserDto>(sql, new { request.UserId });
        }
    }
}
