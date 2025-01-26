using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Configuration.Queries;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.GetUserRegistration;

internal class GetUserRegistrationQueryHandler(ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetUserRegistrationQuery, UserRegistrationDto>
{
    public Task<UserRegistrationDto> Handle(GetUserRegistrationQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql =
            """
            SELECT
                Id,
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