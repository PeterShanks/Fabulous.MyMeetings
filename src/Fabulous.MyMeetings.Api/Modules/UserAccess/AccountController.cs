using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Api.Modules.UserAccess.Models;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Authorization.GetUserPermissions;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserAccess.Application.Users.GetUser;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.UserAccess
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController(
        IUserAccessModule userAccessModule,
        IUserRegistrationsModule userRegistrationsModule): ControllerBase
    {
        [NoPermissionRequired]
        [HasScope(Scope.User.Authenticate)]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateUserRequest request)
        {
            return Ok(await userAccessModule.ExecuteCommandAsync(request.ToCommand()));
        }

        [NoPermissionRequired]
        [HasScope(Scope.User.Authenticate)]
        [HttpGet("{userId:guid}/permissions")]
        public async Task<IActionResult> GetPermissions(Guid userId)
        {
            return Ok(await userAccessModule.ExecuteQueryAsync(new GetUserPermissionsQuery(userId)));
        }

        [NoPermissionRequired]
        [HasScope(Scope.User.Read)]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            return Ok(await userAccessModule.ExecuteQueryAsync(new GetUserQuery(userId)));
        }

        [NoPermissionRequired]
        [NoScopeRequired]
        [HttpPost]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
        {
            return Ok(await userRegistrationsModule.ExecuteCommandAsync(request.ToCommand()));
        }

        [NoPermissionRequired]
        [NoScopeRequired]
        [HttpPatch("{userRegistrationId:guid}/confirm")]
        public async Task<IActionResult> ConfirmUserRegistration([FromRoute] Guid userRegistrationId, string token)
        {
            await userRegistrationsModule.ExecuteCommandAsync(
                new ConfirmUserRegistrationCommand(userRegistrationId, token)
            );
            return Ok();
        }
    }
}
