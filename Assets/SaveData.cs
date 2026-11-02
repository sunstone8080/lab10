using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransformData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

[Serializable]
public class SaveEntry
{
    public string saveID;
    public string kind;
    public string data;
}

[Serializable]
public class SaveFile
{
    public List<SaveEntry> entries = new List<SaveEntry>();
}

[Serializable]
public class ScoreData
{
    public int score;
}