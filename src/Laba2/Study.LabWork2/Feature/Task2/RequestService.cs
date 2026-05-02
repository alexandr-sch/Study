using Study.LabWork2.Abstractions.Feature.Task2;
using System.Text.Json;

namespace Study.LabWork2.Feature.Task2;


public sealed class RequestService : IRequestService
{
    private readonly HttpClient _httpClient;

    public RequestService()
    {
        _httpClient = new HttpClient();
    }

    public string FetchData(string url)
    {
        try
        {
            var response = _httpClient.GetStringAsync(url).GetAwaiter().GetResult();
            return response;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Ошибка при запросе к {url}: {ex.Message}", ex);
        }
    }

    public async Task<string> FetchDataAsync(string url, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetStringAsync(url, cancellationToken);
            return response;
        }
        catch (HttpRequestException ex)
        {
            throw new InvalidOperationException($"Ошибка при запросе к {url}: {ex.Message}", ex);
        }
    }

    public void Dispose()
    {
        _httpClient?.Dispose();
    }
}
