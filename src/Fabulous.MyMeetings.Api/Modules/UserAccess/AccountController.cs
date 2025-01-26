using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Api.Modules.UserAccess.Models;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.UserAccess
{
    [ApiController]
    [HasScope(Scope.User.Authenticate)]
    [Route("api/user-access/[controller]")]
    public class AccountController(IUserAccessModule module): ControllerBase
    {
        [HttpPost("authenticate")]
        [NoPermissionRequired]
        public async Task<IActionResult> Authenticate(AuthenticateUserRequest request)
        {
            return Ok(await module.ExecuteCommandAsync(request.ToCommand()));
        }

        [HttpGet("{userId:guid}/permissions")]
        [NoPermissionRequired]
        public async Task<IActionResult> GetPermissions(Guid userId)
        {
            return Ok(await module.ExecuteQueryAsync(new GetUserPermissionsQuery(userId)));
        }

        [HttpGet("{userId:guid}")]
        [NoPermissionRequired]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            return Ok(await module.ExecuteQueryAsync(new GetUserQuery(userId)));
        }
    }
}
