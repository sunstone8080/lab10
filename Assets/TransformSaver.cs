using UnityEngine;

public class TransformSaver : SaveableBehaviour
{
    [SerializeField] private string kind = "Player";

    public override string SaveKind =>
        TryGetComponent(out Target t) && !string.IsNullOrEmpty(t.TypeName) ? t.TypeName : kind;

    public override string SavedData
    {
        get
        {
            var data = new TransformData
            {
                position = transform.localPosition,
                rotation = transform.localRotation,
                scale = transform.localScale
            };
            return JsonUtility.ToJson(data);
        }
    }

    public override void LoadFromData(string json)
    {
        var data = JsonUtility.FromJson<TransformData>(json);
        transform.localPosition = data.position;
        transform.localRotation = data.rotation;
        transform.localScale = data.scale;
    }
}