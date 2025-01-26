using Duende.IdentityModel;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace Fabulous.MyMeetings.Identity.UserManagement
{
    public class UserManagementResourceOwnerPasswordValidator(UserManagementService service): IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var request = new AuthenticateRequest(context.UserName, context.Password);

            var result = await service.AuthenticateUserAsync(request);

            context.Result = result.IsAuthenticated 
                ? new GrantValidationResult(result.UserId.ToString(), OidcConstants.AuthenticationMethods.Password) 
                : new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
        }
    }
}
