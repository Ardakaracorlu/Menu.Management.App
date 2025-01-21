using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using Menu.Management.App.Model.Response.Account;

namespace Menu.Management.App.Services.Auth
{
    public class AuthService
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly NavigationManager _navigationManager;

        public AuthService(ProtectedSessionStorage sessionStorage, NavigationManager navigationManager)
        {
            _sessionStorage = sessionStorage;
            _navigationManager = navigationManager;
        }
        public async Task SetLoginSessionAsync(LoginResponse response)
        {
            await _sessionStorage.SetAsync("authToken", response.AccessToken);
            await _sessionStorage.SetAsync("tokenExpiration", 60);
        }

        public async Task<string> GetTokenAsync()
        {
            var result = await _sessionStorage.GetAsync<string>("authToken");
            return result.Success ? result.Value : null;
        }

        public async Task LogoutAsync()
        {
            await _sessionStorage.DeleteAsync("authToken");
            await _sessionStorage.DeleteAsync("tokenExpiration");
            _navigationManager.NavigateTo("/login", forceLoad: true);
        }

        public async Task<bool> IsTokenValidAsync()
        {
            var expirationResult = await _sessionStorage.GetAsync<DateTime>("tokenExpiration");

            if (!expirationResult.Success || expirationResult.Value <= DateTime.UtcNow)
            {
                await LogoutAsync();
                return false;
            }

            return true;
        }
    }
}
