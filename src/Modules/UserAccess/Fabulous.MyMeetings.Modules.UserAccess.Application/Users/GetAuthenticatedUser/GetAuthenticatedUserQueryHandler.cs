using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetAuthenticatedUser;

internal class GetAuthenticatedUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
    IUserContext userContext) : IQueryHandler<GetAuthenticatedUserQuery, UserDto>
{
    public Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
        var connection = sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT" +
                           "[User].[Id], " +
                           "[User].[IsActive], " +
                           "[User].[Email], " +
                           "[User].[Name] " +
                           "FROM [users].[v_Users] AS [User] " +
                           "WHERE [User].[Id] = @UserId";

        return connection.QuerySingleAsync<UserDto>(sql, new
        {
            UserId = userContext.UserId.Value
        });
    }
}