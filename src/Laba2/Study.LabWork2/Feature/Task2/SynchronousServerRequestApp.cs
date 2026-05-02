using System.Diagnostics;
using System.Text.Json;
using Study.LabWork2.Abstractions.Feature.Task2;
using Study.LabWork2.Abstractions.Feature.Task2.DtoModels;

namespace Study.LabWork2.Feature.Task2;

/// <summary>
/// Синхронная версия приложения (без использования async/await)
/// </summary>
public sealed class SynchronousServerRequestApp : IServerRequestApp
{
    private readonly IRequestService _requestService;

    public SynchronousServerRequestApp(IRequestService requestService)
    {
        _requestService = requestService;
    }

    public ExecutionResultDto<TResponse> ExecuteRequests<TResponse>(ServerConfigDto[] servers)
    {
        var responses = new List<TResponse>();
        int successfulRequests = 0;
        int failedRequests = 0;

        var sw = Stopwatch.StartNew();

        foreach (var server in servers)
        {
            try
            {
                Console.WriteLine($"Отправка запроса к {server.Name} ({server.Url})...");

                var jsonResponse = _requestService.FetchData(server.Url);

                Console.WriteLine($"Получен ответ от {server.Name}:");

                var response = JsonSerializer.Deserialize<TResponse>(jsonResponse);

                if (response != null)
                {
                    responses.Add(response);
                    successfulRequests++;
                }
                else
                {
                    Console.WriteLine($"Ошибка: пустой ответ от {server.Name}");
                    failedRequests++;
                }

                var prettyJson = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine(prettyJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при запросе к {server.Name}: {ex.Message}");
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

    public string GetVersion() => "Синхронная";
}
