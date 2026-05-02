using System.Text.Json;
using Study.LabWork2.Abstractions.Feature.Task2;
using Study.LabWork2.Abstractions.Feature.Task2.DtoModels;
using Study.LabWork2.Feature.Task2;

namespace Study.LabWork2.UnitTests.Feature.Task2;

[TestFixture]
public sealed class AsynchronousServerRequestAppTests
{
    private const string UserJson = @"{""id"":1,""name"":""Test User""}";
    private const string PostJson = @"{""id"":2,""title"":""Test Post""}";
    private const string TodoJson = @"{""id"":3,""title"":""Test Todo""}";

    private static IRequestService CreateFakeService()
    {
        return new FakeRequestService(new Dictionary<string, string>
        {
            { "https://test.com/users/1", UserJson },
            { "https://test.com/posts/1", PostJson },
            { "https://test.com/todos/1", TodoJson }
        });
    }

    [Test]
    public void ExecuteRequests_ReturnsAllResponses()
    {
        var service = CreateFakeService();
        var app = new AsynchronousServerRequestApp(service);
        var servers = new ServerConfigDto[]
        {
            new() { Name = "Test1", Url = "https://test.com/users/1" },
            new() { Name = "Test2", Url = "https://test.com/posts/1" },
            new() { Name = "Test3", Url = "https://test.com/todos/1" }
        };

        var result = app.ExecuteRequests<JsonElement>(servers);

        Assert.That(result.Responses, Has.Count.EqualTo(3));
        Assert.That(result.SuccessfulRequests, Is.EqualTo(3));
        Assert.That(result.FailedRequests, Is.EqualTo(0));
    }

    [Test]
    public void ExecuteRequests_HasExecutionTime()
    {
        var service = CreateFakeService();
        var app = new AsynchronousServerRequestApp(service);
        var servers = new ServerConfigDto[]
        {
            new() { Name = "Test", Url = "https://test.com/users/1" }
        };

        var result = app.ExecuteRequests<JsonElement>(servers);

        Assert.That(result.TotalExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
    }

    [Test]
    public void GetVersion_ReturnsCorrectName()
    {
        var service = CreateFakeService();
        var app = new AsynchronousServerRequestApp(service);

        Assert.That(app.GetVersion(), Is.EqualTo("Асинхронная"));
    }

    [Test]
    public void ExecuteRequests_WithInvalidUrl_HandlesError()
    {
        var service = CreateFakeService();
        var app = new AsynchronousServerRequestApp(service);
        var servers = new ServerConfigDto[]
        {
            new() { Name = "Invalid", Url = "https://test.com/invalid" }
        };

        var result = app.ExecuteRequests<JsonElement>(servers);

        Assert.That(result.FailedRequests, Is.GreaterThanOrEqualTo(1));
        Assert.That(result.SuccessfulRequests, Is.EqualTo(0));
    }

    private sealed class FakeRequestService : IRequestService
    {
        private readonly Dictionary<string, string> _responses;

        public FakeRequestService(Dictionary<string, string> responses)
        {
            _responses = responses;
        }

        public string FetchData(string url)
        {
            if (_responses.TryGetValue(url, out var response))
                return response;
            throw new InvalidOperationException($"Ошибка запроса к {url}: 404 Not Found");
        }

        public Task<string> FetchDataAsync(string url, CancellationToken cancellationToken = default)
        {
            if (_responses.TryGetValue(url, out var response))
                return Task.FromResult(response);
            throw new InvalidOperationException($"Ошибка запроса к {url}: 404 Not Found");
        }
    }
}
