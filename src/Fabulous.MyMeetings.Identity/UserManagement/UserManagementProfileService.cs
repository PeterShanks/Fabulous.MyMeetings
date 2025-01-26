using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

namespace Fabulous.MyMeetings.Identity.UserManagement
{
    public class UserManagementProfileService(UserManagementService service): IProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var requestClaims = context.RequestedClaimTypes.ToList();

            var useId = Guid.Parse(context.Subject.GetSubjectId());

            var user = await service.GetUserAsync(useId);

            var claims = GetSupportedClaims(user, requestClaims);

            if (requestClaims.Contains(Constants.CustomClaims.Permissions))
            {
                var permissions = await service.GetUserPermissionsAsync(useId);
                claims.Add(new Claim(Constants.CustomClaims.Permissions, string.Join(" ", permissions)));
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var useId = Guid.Parse(context.Subject.GetSubjectId());

            var user = await service.GetUserAsync(useId);

            context.IsActive = user.IsActive;
        }

        private List<Claim> GetSupportedClaims(UserResponse user, List<string> requestedClaimTypes)
        {
            var claims = new List<Claim>();

            if (requestedClaimTypes.Any())
            {
                if (requestedClaimTypes.Contains(ClaimTypes.Name))
                {
                    claims.Add(new Claim(ClaimTypes.Name, user.Name));
                }
                if (requestedClaimTypes.Contains(ClaimTypes.Email))
                {
                    claims.Add(new Claim(ClaimTypes.Email, user.Email));
                }
            }

            return claims;
        }
    }
}
