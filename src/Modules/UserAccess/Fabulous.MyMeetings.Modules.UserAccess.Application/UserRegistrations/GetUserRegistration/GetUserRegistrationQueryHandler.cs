using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.UserRegistrations.GetUserRegistration
{
    internal class GetUserRegistrationQueryHandler: IQueryHandler<GetUserRegistrationQuery, UserRegistrationDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUserRegistrationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<UserRegistrationDto> Handle(GetUserRegistrationQuery request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql =
                """
                SELECT
                    Id,
                    Login,
                    Email,
                    FirstName,
                    LastName,
                    Name,
                    StatusCode
                FROM Users.v_UserRegistrations
                WHERE Id = @UserRegistrationId
                """;

            return connection.QuerySingleAsync<UserRegistrationDto>(sql, new { request.UserRegistrationId });
        }
    }
}
