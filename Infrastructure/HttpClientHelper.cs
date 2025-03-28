using CurrieTechnologies.Razor.SweetAlert2;
using Menu.Management.App.Model.Response;
using Menu.Management.App.Services.Auth;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Menu.Management.App.Infrastructure;

public class HttpClientHelper
{
    private readonly HttpClient _httpClient;
    private readonly SweetAlertService _swal;
    private readonly NavigationManager _navigationManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthService _authService;

    public HttpClientHelper(HttpClient httpClient, SweetAlertService swal, NavigationManager navigationManager, IHttpContextAccessor httpContextAccessor, AuthService authService)
    {
        _httpClient = httpClient;
        _swal = swal;
        _navigationManager = navigationManager;
        _httpContextAccessor = httpContextAccessor;
        _authService = authService;
    }

    private async Task HandleErrorAsync(HttpResponseMessage response)
    {
        string responseBody = await response.Content.ReadAsStringAsync();

        try
        {
            var baseErrorResponse = JsonConvert.DeserializeObject<BaseErrorResponse>(responseBody);

            await _swal.FireAsync(new SweetAlertOptions
            {
                Title = "Hata",
                Text = baseErrorResponse?.Detail ?? "Bilinmeyen bir hata oluştu.",
                Icon = SweetAlertIcon.Error,
                Timer = 5000,
                ConfirmButtonText = "Tamam",
            });

            // 401 durumunda oturumu temizle ve login sayfasına yönlendir
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                ClearSessionAndRedirect();
            }
        }
        catch (Exception)
        {
            await _swal.FireAsync(new SweetAlertOptions
            {
                Title = "Hata",
                Text = $"Sunucudan alınan beklenmeyen bir yanıt: {responseBody}",
                Icon = SweetAlertIcon.Error,
                Timer = 5000,
                ConfirmButtonText = "Tamam"
            });
        }
    }

    private void ClearSessionAndRedirect()
    {
        _httpContextAccessor.HttpContext?.Session.Clear();
        _navigationManager.NavigateTo("/login", forceLoad: true);
    }

    private async Task<HttpResponseMessage> SendRequestAsync(Func<Task<HttpResponseMessage>> apiCall)
    {
        try
        {
            var response = await apiCall();
            if (!response.IsSuccessStatusCode)
            {
                await HandleErrorAsync(response);
            }

            return response;
        }
        catch (Exception ex)
        {
            await _swal.FireAsync(new SweetAlertOptions
            {
                Title = "Bağlantı Hatası",
                Text = $"Bir hata oluştu: {ex.Message}",
                Icon = SweetAlertIcon.Error,
                Timer = 5000,
                ConfirmButtonText = "Tamam",
            });

            throw;
        }
    }

    public async Task<HttpResponseMessage> GetAsync(string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
            await AddAuthorizationHeader();       
            return await _httpClient.GetAsync(url);
        });
    }

    public async Task<HttpResponseMessage> PostAsync(object? requestModel, string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
            await AddAuthorizationHeader();       
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
            return await _httpClient.PostAsync(url, requestModel != null ? requestContent : null);
        });
    }

    public async Task<HttpResponseMessage> PutAsync(object requestModel, string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
            await AddAuthorizationHeader();        
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
            return await _httpClient.PutAsync(url, requestContent);
        });
    }

    public async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
            await AddAuthorizationHeader();        
            return await _httpClient.DeleteAsync(url);
        });
    }

    private void AddHeaders(Dictionary<string, string>? headers)
    {
        _httpClient.DefaultRequestHeaders.Clear();

        if (headers != null)
        {
            foreach (var header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }
    }

    private async Task AddAuthorizationHeader()
    {
        var token = await _authService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
