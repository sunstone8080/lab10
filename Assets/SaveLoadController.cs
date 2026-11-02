using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SaveLoadController : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TargetSpawner spawner;

    private const string LOCATIONS_FILE = "locations.json";
    private const string SCORE_FILE = "score.dat";

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.sKey.wasPressedThisFrame) Save();
        if (kb.lKey.wasPressedThisFrame) Load();
    }

    private void Save()
    {
        SavingService.SaveLocations(LOCATIONS_FILE);
        SavingService.SaveScore(SCORE_FILE, scoreManager.Score);
    }

    private void Load()
    {

        if (SavingService.TryLoadScore(SCORE_FILE, out int score))
            scoreManager.SetScore(score);

        SaveFile file = SavingService.ReadLocations(LOCATIONS_FILE);
        if (file == null) return;

        spawner.ClearTargets();

        var playerSavers = FindObjectsByType<TransformSaver>(FindObjectsSortMode.None)
                           .Where(s => !s.TryGetComponent(out Target _))
                           .ToList();

        foreach (SaveEntry entry in file.entries)
        {
            if (entry.kind == "Player")
            {
                var saver = playerSavers.FirstOrDefault(s => s.SaveID == entry.saveID);
                if (saver != null) saver.LoadFromData(entry.data);
            }
            else
            {

                Target t = spawner.SpawnByType(entry.kind, Vector3.zero, 1);
                if (t == null) continue;

                t.GetComponent<TransformSaver>().LoadFromData(entry.data);
                t.Direction = t.transform.localScale.x >= 0f ? 1 : -1;
            }
        }
    }
}