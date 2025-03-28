using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using Menu.Management.App.Model.Response.Account;
using System.IdentityModel.Tokens.Jwt;

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
            await _sessionStorage.SetAsync("tokenExpiration", 1);
        }
        public async Task ClearSessionAsync()
        {
            await _sessionStorage.DeleteAsync("authToken");
            await _sessionStorage.DeleteAsync("tokenExpiration");
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
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                await ClearSessionAsync();
                return false;
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
            if (expClaim == null)
            {
                await ClearSessionAsync();
                return false;
            }

            // exp claim'i saniye cinsinden epoch time'dır
            var expUnix = long.Parse(expClaim.Value);
            var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;

            if (expDateTime <= DateTime.UtcNow)
            {
                await ClearSessionAsync();
                return false;
            }

            return true;
        }
    }
}
