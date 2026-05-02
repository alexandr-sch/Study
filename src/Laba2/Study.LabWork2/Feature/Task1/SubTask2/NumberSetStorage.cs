using System.Text.Json;

namespace Study.LabWork2.Feature.Task1.SubTask2;

public static class NumberSetStorage
{
    private const string FileName = "number_sets.json";
    private const int SetsCount = 15;
    private const int NumbersPerSet = 100;
    private const int MinValue = 1;
    private const int MaxValue = 100;

    public static List<List<int>> LoadOrGenerate()
    {

        var fullPath = Path.GetFullPath(FileName);
        Console.WriteLine($"Полный путь к файлу: {fullPath}");

        if (File.Exists(FileName))
        {
            var json = File.ReadAllText(FileName);
            var sets = JsonSerializer.Deserialize<List<List<int>>>(json);

            if (sets != null && sets.Count == SetsCount)
            {
                Console.WriteLine($"Загружено {sets.Count} наборов из файла {FileName}");
                return sets;
            }
        }

        var newSets = GenerateSets();
        var newJson = JsonSerializer.Serialize(newSets, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FileName, newJson);
        Console.WriteLine($"Сгенерировано {SetsCount} новых наборов и сохранено в {FileName}");

        return newSets;
    }

    private static List<List<int>> GenerateSets()
    {
        var random = new Random(42); 
        var sets = new List<List<int>>(SetsCount);

        for (int i = 0; i < SetsCount; i++)
        {
            var set = new List<int>(NumbersPerSet);
            for (int j = 0; j < NumbersPerSet; j++)
            {
                set.Add(random.Next(MinValue, MaxValue + 1));
            }
            sets.Add(set);
        }

        return sets;
    }
}
