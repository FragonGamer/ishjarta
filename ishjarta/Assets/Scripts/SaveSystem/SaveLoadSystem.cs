using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    public string SavePath => $"{Application.persistentDataPath}/save.txt";

    [ContextMenu("Save")]
    void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }
    [ContextMenu("Load")]
    void Load()
    {
        var save = LoadFile();
        LoadState(save);
    }
    
    void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, state);
        }
    }

    Dictionary<string,object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found.");
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            return (Dictionary<string, object>)binaryFormatter.Deserialize(stream);
        }
    }

    void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }
    void LoadState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
