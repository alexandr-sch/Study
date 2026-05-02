using System.Diagnostics;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask2.DtoModels;

namespace Study.LabWork2.Feature.Task1.SubTask2;


public sealed class NumberSetProcessor : INumberSetProcessor
{
    private readonly int _maxThreads;
    private readonly Semaphore _semaphore;
    private readonly object _resultsLock = new();
    private readonly Mutex _totalSumMutex = new();

    private readonly List<List<int>> _numberSets;
    private readonly List<ResultEntryDto> _results = new();
    private int _totalSum;
    private TimeSpan _executionTime;

    public NumberSetProcessor(int maxThreads = 4)
    {
        _maxThreads = maxThreads;
        _semaphore = new Semaphore(maxThreads, maxThreads);
        _numberSets = NumberSetStorage.LoadOrGenerate();
    }

    public void Process()
    {
        _results.Clear();
        _totalSum = 0;

        var sw = Stopwatch.StartNew();
        var threads = new List<Thread>();

        for (int i = 0; i < _numberSets.Count; i++)
        {
            int setIndex = i;
            var thread = new Thread(() => ProcessSet(setIndex + 1, _numberSets[setIndex]));
            thread.Start();
            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        sw.Stop();
        _executionTime = sw.Elapsed;
    }

    public ProcessingResultDto GetResult()
    {
        return new ProcessingResultDto
        {
            Results = new List<ResultEntryDto>(_results),
            TotalSum = _totalSum,
            ExecutionTime = _executionTime,
            ProcessedSetsCount = _results.Count
        };
    }
    private void ProcessSet(int setNumber, List<int> numbers)
    {
        _semaphore.WaitOne();

        try
        {
            int sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }

            int threadId = Environment.CurrentManagedThreadId;

            Console.WriteLine($"Набор {setNumber}: сумма = {sum} (поток {threadId})");

            lock (_resultsLock)
            {
                _results.Add(new ResultEntryDto
                {
                    SetNumber = setNumber,
                    Sum = sum,
                    ThreadId = threadId
                });
            }

            _totalSumMutex.WaitOne();
            try
            {
                _totalSum += sum;
            }
            finally
            {
                _totalSumMutex.ReleaseMutex();
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
