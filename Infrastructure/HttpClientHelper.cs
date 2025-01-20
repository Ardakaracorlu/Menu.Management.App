using CurrieTechnologies.Razor.SweetAlert2;
using Menu.Management.App.Configuration;
using Menu.Management.App.Model.Response;
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

    public HttpClientHelper(HttpClient httpClient, SweetAlertService swal, NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _swal = swal;
        _navigationManager = navigationManager;
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
            return await _httpClient.GetAsync(url);
        });
    }

    public async Task<HttpResponseMessage> PostAsync(object? requestModel, string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
            return await _httpClient.PostAsync(url, requestModel != null ? requestContent : null);
        });
    }

    public async Task<HttpResponseMessage> PutAsync(object requestModel, string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
            var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
            return await _httpClient.PutAsync(url, requestContent);
        });
    }

    public async Task<HttpResponseMessage> DeleteAsync(string url, Dictionary<string, string>? headers = null)
    {
        return await SendRequestAsync(async () =>
        {
            AddHeaders(headers);
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

    #region old
    //public static async Task<HttpResponseMessage> Get(string url, Dictionary<string, string>? headers = null)
    //{
    //    using HttpClient _httpClient = new HttpClient();
    //    if (headers != null)
    //    {
    //        foreach (KeyValuePair<string, string> header in headers)
    //        {
    //            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
    //        }
    //    }

    //    var response = await _httpClient.GetAsync(url);
    //    return response;
    //}

    //public static async Task<HttpResponseMessage> Post(object? requestModel, string url, Dictionary<string, string>? headers = null)
    //{
    //    using HttpClient _httpClient = new HttpClient();
    //    var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
    //    if (headers != null)
    //    {
    //        foreach (KeyValuePair<string, string> header in headers)
    //        {
    //            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
    //        }
    //    }

    //    var response = await _httpClient.PostAsync(url, requestModel != null ? requestContent : null);
    //    return response;

    //}

    //public static async Task<HttpResponseMessage> Put(object requestModel, string url, Dictionary<string, string>? headers = null)
    //{
    //    using HttpClient _httpClient = new HttpClient();
    //    var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
    //    if (headers != null)
    //    {
    //        foreach (KeyValuePair<string, string> header in headers)
    //        {
    //            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
    //        }
    //    }

    //    var response = await _httpClient.PutAsync(url, requestContent);
    //    return response;
    //}

    //public static async Task<HttpResponseMessage> Delete(string url, Dictionary<string, string>? headers = null)
    //{
    //    using HttpClient _httpClient = new HttpClient();
    //    if (headers != null)
    //    {
    //        foreach (KeyValuePair<string, string> header in headers)
    //        {
    //            _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
    //        }
    //    }

    //    var response = await _httpClient.DeleteAsync(url);
    //    return response;
    //}

    //public static async Task<HttpResponseMessage> PostFormData(string url, MultipartFormDataContent FormData, Dictionary<string, string>? headers = null)
    //{
    //    HttpClientHandler httpClientHandler = new HttpClientHandler();
    //    using (HttpClient _httpClient = new HttpClient(httpClientHandler))
    //    {
    //        HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
    //        if (headers != null)
    //        {
    //            foreach (var requestHeader in headers)
    //            {
    //                httpRequestMessage.Headers.Add(requestHeader.Key.ToString(), requestHeader.Value);
    //            }
    //        }

    //        httpRequestMessage.Content = FormData;

    //        _httpClient.BaseAddress = new Uri(url);
    //        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

    //        return httpResponseMessage;
    //    }
    //}
    #endregion
}