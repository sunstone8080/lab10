using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    [SerializeField] private float spawnInterval = 1.2f;
    [SerializeField] private float[] rowHeights = { 4f, 2.5f, 1f };

    private TargetDirector director;
    private float timer;

    void Awake()
    {
        director = new TargetDirector(new TargetBuilder(targetPrefab));
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0f) return;
        timer = spawnInterval;

        int row = Random.Range(0, rowHeights.Length);
        int dir = (row % 2 == 0) ? 1 : -1;
        Vector3 pos = new Vector3(-dir * 11f, rowHeights[row], 0f);

        string[] types = { "Duck", "Rabbit", "Owl" };
        SpawnByType(types[Random.Range(0, types.Length)], pos, dir);
    }

    public Target SpawnByType(string typeName, Vector3 pos, int dir)
    {
        switch (typeName)
        {
            case "Duck": return director.BuildDuck(pos, dir);
            case "Rabbit": return director.BuildRabbit(pos, dir);
            case "Owl": return director.BuildOwl(pos, dir);
            default: return null;
        }
    }

    public void ClearTargets()
    {
        foreach (Target t in FindObjectsByType<Target>(FindObjectsSortMode.None))
            Destroy(t.gameObject);
    }
}