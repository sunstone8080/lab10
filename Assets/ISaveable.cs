public interface ISaveable
{
    string SaveID { get; }
    string SaveKind { get; }
    string SavedData { get; }
    void LoadFromData(string json);
}