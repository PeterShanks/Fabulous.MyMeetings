using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Api.Modules.UserAccess.Models;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.Contracts;
using Fabulous.MyMeetings.Modules.UserRegistrations.Application.UserRegistrations.ConfirmUserRegistration;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.UserAccess
{
    [ApiController]
    [Route("api/user-access/[controller]")]
    public class UserRegistrationsController(IUserRegistrationsModule module): ControllerBase
    {
        [HttpPost]
        [NoPermissionRequired]
        [HasScope(Scope.User.Write)]
        public async Task<IActionResult> RegisterNewUser(RegisterNewUserRequest request)
        {
            return Ok(await module.ExecuteCommandAsync(request.ToCommand()));
        }

        [NoPermissionRequired]
        [HttpPatch("{userRegistrationId:guid}/confirm")]
        [HasScope(Scope.User.Write)]
        public async Task<IActionResult> ConfirmUserRegistration(Guid userRegistrationId)
        {
            await module.ExecuteCommandAsync(new ConfirmUserRegistrationCommand(userRegistrationId));
            return Ok();
        }
    }
}
