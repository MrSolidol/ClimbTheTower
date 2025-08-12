using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataSerializer
{
    private BinaryFormatter formatter;
    private string filePath;

    public DataSerializer() 
    {
        InitFilePath();
    }


    public void Save<T>(T data, string fileName)
    {
        string fullPath = Path.Combine(filePath, fileName);
        Directory.CreateDirectory(filePath);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(fullPath, json);
    }

    public T Load<T>(string fileName) where T : new()
    {
        string fullPath = Path.Combine(filePath, fileName);
        if (!File.Exists(fullPath)) { return new T(); }

        string json = File.ReadAllText(fullPath);
        return JsonUtility.FromJson<T>(json);
    }


    private void InitFilePath() 
    {
        filePath = Application.persistentDataPath;
    }
}
