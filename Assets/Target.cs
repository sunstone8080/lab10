using UnityEngine;

public class Target : MonoBehaviour
{
    public delegate void TargetHitHandler(int points);
    public static event TargetHitHandler OnTargetHit;

    public float Speed { get; set; }
    public int Points { get; set; }
    public int Direction { get; set; } = 1;
    public string TypeName { get; set; }

    void Update()
    {
        transform.Translate(Vector3.right * Direction * Speed * Time.deltaTime);
        if (Mathf.Abs(transform.position.x) > 12f) Destroy(gameObject);
    }

    public void Hit()
    {
        OnTargetHit?.Invoke(Points);
        Destroy(gameObject);
    }
}