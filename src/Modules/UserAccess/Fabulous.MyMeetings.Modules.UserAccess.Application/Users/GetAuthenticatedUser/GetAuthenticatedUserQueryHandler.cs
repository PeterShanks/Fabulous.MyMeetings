using Dapper;
using Fabulous.MyMeetings.BuildingBlocks.Application;
using Fabulous.MyMeetings.BuildingBlocks.Application.Data;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Queries;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetAuthenticatedUser;

internal class GetAuthenticatedUserQueryHandler : IQueryHandler<GetAuthenticatedUserQuery, UserDto>
{
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetAuthenticatedUserQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
        IExecutionContextAccessor executionContextAccessor)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _executionContextAccessor = executionContextAccessor;
    }

    public Task<UserDto> Handle(GetAuthenticatedUserQuery request, CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.GetOpenConnection();

        const string sql = "SELECT" +
                           "[User].[Id], " +
                           "[User].[IsActive], " +
                           "[User].[Login], " +
                           "[User].[Email], " +
                           "[User].[Name] " +
                           "FROM [users].[v_Users] AS [User] " +
                           "WHERE [User].[Id] = @UserId";

        return connection.QuerySingleAsync<UserDto>(sql, new
        {
            _executionContextAccessor.UserId
        });
    }
}