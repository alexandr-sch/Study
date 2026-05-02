using Study.LabWork2.Feature.Task1.SubTask2;

namespace Study.LabWork2.UnitTests.Feature.Task1.SubTask2;

[TestFixture]
public class NumberSetProcessorTests
{
    [Test]
    public void Process_Returns15Results()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        Assert.That(result.ProcessedSetsCount, Is.EqualTo(15));
        Assert.That(result.Results, Has.Count.EqualTo(15));
    }

    [Test]
    public void Process_AllSetNumbersAreUnique()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        var setNumbers = result.Results.Select(r => r.SetNumber).ToList();
        Assert.That(setNumbers, Is.Unique);
        Assert.That(setNumbers, Has.All.InRange(1, 15));
    }

    [Test]
    public void Process_TotalSumMatchesSumOfAllSets()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        var sumOfSets = result.Results.Sum(r => r.Sum);
        Assert.That(result.TotalSum, Is.EqualTo(sumOfSets));
    }

    [Test]
    public void Process_ExecutionTimeIsPositive()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        Assert.That(result.ExecutionTime.TotalMilliseconds, Is.GreaterThan(0));
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(4)]
    [TestCase(8)]
    public void Process_DifferentMaxThreads_Works(int maxThreads)
    {
        var processor = new NumberSetProcessor(maxThreads);
        processor.Process();
        var result = processor.GetResult();

        Assert.That(result.Results, Has.Count.EqualTo(15));
        Assert.That(result.TotalSum, Is.GreaterThan(0));
    }

    [Test]
    public void Process_EachSumInValidRange()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        foreach (var entry in result.Results)
        {
            Assert.That(entry.Sum, Is.InRange(100, 10000),
                $"Сумма набора {entry.SetNumber} должна быть от 100 до 10000");
        }
    }

    [Test]
    public void Process_ResultsUseMultipleThreads()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        var uniqueThreads = result.Results.Select(r => r.ThreadId).Distinct().Count();
        Assert.That(uniqueThreads, Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public void Process_AllEntriesHaveValidData()
    {
        var processor = new NumberSetProcessor(4);
        processor.Process();
        var result = processor.GetResult();

        foreach (var entry in result.Results)
        {
            Assert.That(entry.SetNumber, Is.InRange(1, 15));
            Assert.That(entry.Sum, Is.GreaterThan(0));
            Assert.That(entry.ThreadId, Is.GreaterThan(0));
            Assert.That(entry.ToString(), Does.Contain($"Набор {entry.SetNumber}"));
            Assert.That(entry.ToString(), Does.Contain($"сумма = {entry.Sum}"));
        }
    }

    [Test]
    public void Process_GetResultBeforeProcess_ReturnsEmpty()
    {
        var processor = new NumberSetProcessor(4);
        var result = processor.GetResult();

        Assert.That(result.ProcessedSetsCount, Is.EqualTo(0));
        Assert.That(result.Results, Is.Empty);
        Assert.That(result.TotalSum, Is.EqualTo(0));
    }

    [Test]
    public void Process_CanRunMultipleTimes()
    {
        var processor = new NumberSetProcessor(4);

        processor.Process();
        var result1 = processor.GetResult();
        Assert.That(result1.Results, Has.Count.EqualTo(15));

        processor.Process();
        var result2 = processor.GetResult();
        Assert.That(result2.Results, Has.Count.EqualTo(15));
        Assert.That(result2.TotalSum, Is.EqualTo(result1.TotalSum));
    }
}
