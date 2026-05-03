using Study.LabWork2.Abstractions.Feature.Task1.SubTask1;
using Study.LabWork2.Feature.Task1.SubTask1;

namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask1;

[TestFixture]
public class MutexServiceTests
{
    private IPrimeCounter CreateCounter() => new MutexService();

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(8)]
    public void CountPrimes_StandardRange_Returns1229(int threadCount)
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(1, 10000, threadCount);

        Assert.That(result.PrimeCount, Is.EqualTo(1229));
    }

    [Test]
    [TestCase(1, 10, 4, 4)]   
    [TestCase(1, 20, 4, 8)]    
    [TestCase(10, 30, 2, 6)]   
    [TestCase(1, 2, 1, 1)]    
    public void CountPrimes_SmallRange_ReturnsCorrectCount(int start, int end, int threadCount, int expectedCount)
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(start, end, threadCount);

        Assert.That(result.PrimeCount, Is.EqualTo(expectedCount));
    }

    [Test]
    public void CountPrimes_ResultContainsCorrectMetadata()
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(1, 1000, 4);

        Assert.That(result.ThreadCount, Is.EqualTo(4));
        Assert.That(result.SynchronizationType, Is.EqualTo("Mutex"));
        Assert.That(result.ExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
        Assert.That(result.FoundPrimes, Is.Not.Null);
        Assert.That(result.FoundPrimes, Has.Count.EqualTo(result.PrimeCount));
    }

    [Test]
    public void CountPrimes_AllFoundNumbersArePrime()
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(1, 1000, 4);

        foreach (var prime in result.FoundPrimes)
        {
            Assert.That(PrimeChecker.IsPrime(prime), Is.True, $"Число {prime} не является простым");
        }
    }

    [Test]
    public void CountPrimes_RangeWithoutPrimes_ReturnsZero()
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(8, 10, 2); 

        Assert.That(result.PrimeCount, Is.EqualTo(0));
        Assert.That(result.FoundPrimes, Is.Empty);
    }

    [Test]
    public void CountPrimes_SingleThread_Works()
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(1, 100, 1);

        Assert.That(result.ThreadCount, Is.EqualTo(1));
        Assert.That(result.PrimeCount, Is.GreaterThan(0));
    }

    [Test]
    public void CountPrimes_GetVersionName_ReturnsCorrectName()
    {
        var counter = CreateCounter();

        Assert.That(counter.GetVersionName(), Is.EqualTo("Mutex"));
    }

    [Test]
    public void CountPrimes_ResultToString_ContainsKeyInfo()
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(1, 100, 2);
        var str = result.ToString();

        Assert.That(str, Does.Contain("Mutex"));
        Assert.That(str, Does.Contain("Найдено простых чисел"));
        Assert.That(str, Does.Contain("Время выполнения"));
    }

    [Test]
    public void CountPrimes_ResultIsValid_WithExpected1229()
    {
        var counter = CreateCounter();
        var result = counter.CountPrimes(1, 10000, 4);

        Assert.That(result.IsValid(1229), Is.True);
    }
}
