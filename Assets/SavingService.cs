using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

#pragma warning disable SYSLIB0011

public static class SavingService
{
    public static string PathFor(string fileName) =>
        Path.Combine(Application.persistentDataPath, fileName);

    public static void SaveLocations(string fileName)
    {
        var file = new SaveFile();

        var saveables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                              .OfType<ISaveable>();
        foreach (var s in saveables)
        {
            file.entries.Add(new SaveEntry
            {
                saveID = s.SaveID,
                kind = s.SaveKind,
                data = s.SavedData
            });
        }

        File.WriteAllText(PathFor(fileName), JsonUtility.ToJson(file, true));
        Debug.Log($"Saved {file.entries.Count} objects to {PathFor(fileName)}");
    }

    public static SaveFile ReadLocations(string fileName)
    {
        string path = PathFor(fileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"No save file at {path}");
            return null;
        }
        return JsonUtility.FromJson<SaveFile>(File.ReadAllText(path));
    }

    public static void SaveScore(string fileName, int score)
    {
        var data = new ScoreData { score = score };
        var formatter = new BinaryFormatter();
        using (var stream = new FileStream(PathFor(fileName), FileMode.Create))
        {
            formatter.Serialize(stream, data);
        }
        Debug.Log($"Saved score {score} to {PathFor(fileName)}");
    }

    public static bool TryLoadScore(string fileName, out int score)
    {
        score = 0;
        string path = PathFor(fileName);
        if (!File.Exists(path))
        {
            Debug.LogWarning($"No score file at {path}");
            return false;
        }

        var formatter = new BinaryFormatter();
        using (var stream = new FileStream(path, FileMode.Open))
        {
            var data = (ScoreData)formatter.Deserialize(stream);
            score = data.score;
        }
        return true;
    }
}