using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;

namespace Menu.Management.App.Infrastructure;
public static class HttpClientHelper
{
    public static async Task<HttpResponseMessage> Get(string url, Dictionary<string, string>? headers = null)
    {
        using HttpClient _httpClient = new HttpClient();
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var response = await _httpClient.GetAsync(url);
        return response;
    }

    public static async Task<HttpResponseMessage> Post(object? requestModel, string url, Dictionary<string, string>? headers = null)
    {
        using HttpClient _httpClient = new HttpClient();
        var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var response = await _httpClient.PostAsync(url, requestModel != null ? requestContent : null);
        return response;

    }

    public static async Task<HttpResponseMessage> Put(object requestModel, string url, Dictionary<string, string>? headers = null)
    {
        using HttpClient _httpClient = new HttpClient();
        var requestContent = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, MediaTypeNames.Application.Json);
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var response = await _httpClient.PutAsync(url, requestContent);
        return response;
    }

    public static async Task<HttpResponseMessage> Delete(string url, Dictionary<string, string>? headers = null)
    {
        using HttpClient _httpClient = new HttpClient();
        if (headers != null)
        {
            foreach (KeyValuePair<string, string> header in headers)
            {
                _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var response = await _httpClient.DeleteAsync(url);
        return response;
    }

    public static async Task<HttpResponseMessage> PostFormData(string url, MultipartFormDataContent FormData, Dictionary<string, string>? headers = null)
    {
        HttpClientHandler httpClientHandler = new HttpClientHandler();
        using (HttpClient _httpClient = new HttpClient(httpClientHandler))
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            if (headers != null)
            {
                foreach (var requestHeader in headers)
                {
                    httpRequestMessage.Headers.Add(requestHeader.Key.ToString(), requestHeader.Value);
                }
            }

            httpRequestMessage.Content = FormData;

            _httpClient.BaseAddress = new Uri(url);
            var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            return httpResponseMessage;
        }
    }

}