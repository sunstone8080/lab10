using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public delegate void ScoreChangedHandler(int newScore);
    public event ScoreChangedHandler OnScoreChanged;

    public int Score { get; private set; }

    void OnEnable() { Target.OnTargetHit += HandleTargetHit; }
    void OnDisable() { Target.OnTargetHit -= HandleTargetHit; }

    private void HandleTargetHit(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    public void SetScore(int value)
    {
        Score = value;
        OnScoreChanged?.Invoke(Score);
    }
}