using System.Net.Http.Headers;

namespace PocketWeb.Services;

public interface IPocketApiService
{
    Task<T?> GetAsync<T>(string endpoint);
    Task<T?> PostAsync<T, V>(string endpoint, V data);
    Task<T?> PutAsync<T, V>(string endpoint, V data);
    Task<bool> DeleteAsync(string endpoint);
    Task<byte[]> ExportExpense(string endpoint);
}

public class PocketApiService(
    ILogger<PocketApiService> logger,
    AuthentificationService authentificationService,
    HttpClient apiClient,
    IConfiguration configuration) : IPocketApiService
{
    private readonly ILogger<PocketApiService> _logger = logger;
    private readonly AuthentificationService _authentificationService = authentificationService;
    private readonly HttpClient _client = apiClient;
    private readonly IConfiguration _configuration = configuration;

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                HttpClient client = await GetHttpClient();
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"GET {endpoint} failed with status {response.StatusCode}");
                    return default;
                }
                _logger.LogInformation($"GET {endpoint} succeeded with status {response.StatusCode}");

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in GET {endpoint}");
                return default;
            }
        }

        public async Task<T?> PostAsync<T, V>(string endpoint, V data)
        {
            try
            {
                var httpClient = await GetHttpClient();
                var response = await httpClient.PostAsJsonAsync(endpoint, data);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"POST {endpoint} failed with status {response.RequestMessage}");
                    return default;
                }
                _logger.LogInformation($"POST {endpoint} succeeded with status {response.StatusCode}");

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in POST {endpoint}");
                return default;
            }
        }

        public async Task<T?> PutAsync<T, V>(string endpoint, V data)
        {
            try
            {
                var httpClient = await GetHttpClient();
                var response = await httpClient.PutAsJsonAsync(endpoint, data);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"PUT {endpoint} failed with status {response.RequestMessage}");
                    return default;
                }
                _logger.LogInformation($"PUT {endpoint} succeeded with status {response.StatusCode}");

                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in PUT {endpoint}");
                return default;
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var httpClient = await GetHttpClient();
                var response = await httpClient.DeleteAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"DELETE {endpoint} failed with status {response.StatusCode}");
                    return false;
                }
                _logger.LogInformation($"DELETE {endpoint} succeeded with status {response.StatusCode}");

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in DELETE {endpoint}");
                return false;
            }
        }

        public async Task<byte[]> ExportExpense(string endpoint)
        {
            try
            {
                HttpClient client = await GetHttpClient();
                var response = await client.GetAsync(endpoint);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Export {endpoint} failed with status {response.StatusCode}");
                    return [];
                }
                _logger.LogInformation($"Export {endpoint} succeeded with status {response.StatusCode}");

                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in Export {endpoint}");
                return [];
            }
        }

    private async Task<HttpClient> GetHttpClient()
    {
        HttpClient client = new HttpClient();
        string apiUrl = _configuration.GetValue<string>("PocketApiUrl") ?? "https://localhost:7215";
        string? token = await _authentificationService.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.BaseAddress = new Uri($"{apiUrl}/api/v1/");
        return client;
    }
}