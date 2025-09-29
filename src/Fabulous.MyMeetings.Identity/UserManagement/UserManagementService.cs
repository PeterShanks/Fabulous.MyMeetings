using Microsoft.Extensions.Options;

namespace Fabulous.MyMeetings.Identity.UserManagement;

public class UserManagementService(HttpClient httpClient, IOptions<UserManagementServiceSettings> options)
{
    public async Task<AuthenticationResult> AuthenticateUserAsync(AuthenticateRequest request)
    {
        var response = await httpClient.PostAsJsonAsync(options.Value.AuthenticateUrl, request);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<AuthenticationResult>())!;
    }

    public async Task<List<string>> GetUserPermissionsAsync(Guid userId)
    {
        var response = await httpClient.GetAsync(options.Value.GetPermissionsUrl(userId));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<string>>())!;
    }

    public async Task<UserResponse> GetUserAsync(Guid userId)
    {
        var response = await httpClient.GetAsync(options.Value.GetUserUrl(userId));
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<UserResponse>())!;
    }
}