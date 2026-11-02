using UnityEngine;

public abstract class SaveableBehaviour : MonoBehaviour, ISaveable, ISerializationCallbackReceiver
{
    [SerializeField] private string _saveID;

    public string SaveID => _saveID;

    public abstract string SaveKind { get; }
    public abstract string SavedData { get; }
    public abstract void LoadFromData(string json);

    public void OnBeforeSerialize()
    {
        if (string.IsNullOrEmpty(_saveID))
            _saveID = System.Guid.NewGuid().ToString();
    }

    public void OnAfterDeserialize() { }
}