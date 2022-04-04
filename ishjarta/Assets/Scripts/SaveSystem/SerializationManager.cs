using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{
    public static string dataPath = Application.dataPath;
    public static string savePath = dataPath + @"/saves";
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        //Debug.Log(savePath);

        if (!Directory.Exists(savePath))
            Directory.CreateDirectory(savePath);

        FileStream fileStream = File.Create(savePath + @"\" + saveName + ".save");
        Debug.Log("File " + saveName + ".save was created");

        binaryFormatter.Serialize(fileStream, saveData);

        fileStream.Close();

        return true;
    }

    public static object Load(string path, out bool loaded)
    {
        if (!File.Exists(path))
        {
            Debug.Log("Path: "+path+" does not exist");
            loaded = false;
            return null;            
        }
            

        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        FileStream fileStream = File.Open(path, FileMode.Open);

        try
        {
            object save = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            loaded = true;
            return save;
        }
        catch
        {
            Debug.LogError($"Failed to load file at path: {path}");
            fileStream.Close();
            loaded = false;
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
