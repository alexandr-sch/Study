namespace Study.LabWork2.Feature.Task1.SubTask1;

/// <summary>
/// Вспомогательный класс с общими методами для всех версий счетчиков
/// </summary>
public static class PrimeChecker
{
    public static bool IsPrime(int number)
    {
        if (number < 2) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var limit = (int)Math.Sqrt(number);
        for (int i = 3; i <= limit; i += 2)
        {
            if (number % i == 0) return false;
        }
        return true;
    }

 
    public static List<(int start, int end)> SplitRange(int start, int end, int threadCount)
    {
        var ranges = new List<(int start, int end)>();
        int totalNumbers = end - start + 1;
        int numbersPerThread = totalNumbers / threadCount;
        int remainder = totalNumbers % threadCount;

        int currentStart = start;
        for (int i = 0; i < threadCount; i++)
        {
            int currentEnd = currentStart + numbersPerThread - 1;
            if (i < remainder)
            {
                currentEnd++;
            }
            ranges.Add((currentStart, currentEnd));
            currentStart = currentEnd + 1;
        }
        return ranges;
    }
}
