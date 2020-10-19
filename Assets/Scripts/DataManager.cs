using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string file = "player.txt";
    public Character data;
    public void Save(object data)
    {
        string json = JsonUtility.ToJson(data);
        WriteToFile(file, json);

    }

    public void Load()
    {
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFile(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFile(fileName);
        if (File.Exists(path))
        {

            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            return "";
        }
    }

    private string GetFile(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
