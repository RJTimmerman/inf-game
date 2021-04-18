using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string path = Application.persistentDataPath + "/save.game";
    
    
    public static void SaveLevel(LevelData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log("New safe file created at " + path);
    }

    public static LevelData LoadLevel()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = (LevelData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        } else
        {
            Debug.LogWarning("Safe file not found at " + path);
            return null;
        }
    }

    public static void DeleteSave()
    {
        File.Delete(path);
        Debug.Log("Safe file deleted at " + SaveSystem.path);
    }
}
