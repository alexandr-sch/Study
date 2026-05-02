using System.Diagnostics;
using System.Text.Json;
using Study.LabWork2.Abstractions.Feature.Task2;
using Study.LabWork2.Abstractions.Feature.Task2.DtoModels;

namespace Study.LabWork2.Feature.Task2;

/// <summary>
/// Асинхронная версия приложения (с использованием async/await)
/// </summary>
public sealed class AsynchronousServerRequestApp : IServerRequestApp
{
    private readonly IRequestService _requestService;

    public AsynchronousServerRequestApp(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public ExecutionResultDto<TResponse> ExecuteRequests<TResponse>(ServerConfigDto[] servers)
    {
        return ExecuteRequestsAsync<TResponse>(servers).GetAwaiter().GetResult();
    }

    public string GetVersion() => "Асинхронная";

    private async Task<ExecutionResultDto<TResponse>> ExecuteRequestsAsync<TResponse>(ServerConfigDto[] servers)
    {
        var responses = new List<TResponse>();
        int successfulRequests = 0;
        int failedRequests = 0;

        var sw = Stopwatch.StartNew();

        var tasks = servers.Select(server => ProcessRequestAsync<TResponse>(server)).ToArray();

        var results = await Task.WhenAll(tasks);

        foreach (var (response, isSuccess) in results)
        {
            if (isSuccess)
            {
                responses.Add(response!);
                successfulRequests++;
            }
            else
            {
                failedRequests++;
            }
        }

        sw.Stop();

        return new ExecutionResultDto<TResponse>
        {
            Responses = responses,
            TotalExecutionTime = sw.Elapsed,
            SuccessfulRequests = successfulRequests,
            FailedRequests = failedRequests,
            Version = GetVersion()
        };
    }

    private async Task<(TResponse? Response, bool IsSuccess)> ProcessRequestAsync<TResponse>(ServerConfigDto server)
    {
        try
        {
            Console.WriteLine($"Отправка асинхронного запроса к {server.Name} ({server.Url})...");

            var jsonResponse = await _requestService.FetchDataAsync(server.Url);

            Console.WriteLine($"Получен ответ от {server.Name}:");

            var response = JsonSerializer.Deserialize<TResponse>(jsonResponse);

            var prettyJson = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(prettyJson);

            return (response, true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при запросе к {server.Name}: {ex.Message}");
            return (default,false);
        }
    }
}
