using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    //public static string dataPath = Application.persistentDataPath;
    public static string dataPath = @"C:\Users\Fragon\Documents";
    public static string savePath = dataPath + @"\saves";
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        Debug.Log(savePath);

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        FileStream fileStream = File.Create(savePath + @"\" + saveName + ".save");

        binaryFormatter.Serialize(fileStream, saveData);

        fileStream.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
            return null;

        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        FileStream fileStream = File.Open(path, FileMode.Open);

        try
        {
            object save = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return save;
        } catch
        {
            Debug.LogError($"Failed to load file at path: {path}");
            fileStream.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        Vector2SerializationSurrogate v2ss = new Vector2SerializationSurrogate();

        selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), v2ss);

        binaryFormatter.SurrogateSelector = selector;

        return binaryFormatter;
    }
}
