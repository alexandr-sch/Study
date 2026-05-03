using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Abstractions.Feature.Task1.SubTask1.DtoModels;
using System.Diagnostics;

namespace Study.LabWork2.Feature.Task1.SubTask1;

/// <summary>
/// Версия 2. Mutex 
/// </summary>
public sealed class MutexService : IPrimeCounter
{
    private readonly Mutex _mutex = new();
    private int _totalPrimes;
    private List<int> _foundPrimes = new();

    public PrimeCountResultDto CountPrimes(int start, int end, int threadCount)
    {
        _totalPrimes = 0;
        _foundPrimes = new List<int>();
        var ranges = PrimeChecker.SplitRange(start, end, threadCount);
        var threads = new Thread[threadCount];

        var sw = Stopwatch.StartNew();

        for (int i = 0; i < threadCount; i++)
        {
            int threadIndex = i;
            var (s, e) = ranges[i];
            threads[i] = new Thread(() => ProcessRange(threadIndex + 1, s, e));
            threads[i].Start();
        }

        foreach (var t in threads)
        {
            t.Join();
        }

        sw.Stop();

        return new PrimeCountResultDto
        {
            PrimeCount = _totalPrimes,
            ExecutionTime = sw.Elapsed,
            ThreadCount = threadCount,
            SynchronizationType = GetVersionName(),
            FoundPrimes = _foundPrimes
        };
    }

    public string GetVersionName() => "Mutex";

    private void ProcessRange(int threadNumber, int start, int end)
    {
        int localCount = 0;
        var localPrimes = new List<int>();

        for (int i = start; i <= end; i++)
        {
            if (PrimeChecker.IsPrime(i))
            {
                localCount++;
                localPrimes.Add(i);
                Console.WriteLine($"Поток #{threadNumber}: число {i} - простое");
            }
        }

        _mutex.WaitOne();
        try
        {
            _totalPrimes += localCount;
            _foundPrimes.AddRange(localPrimes);
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }
}
