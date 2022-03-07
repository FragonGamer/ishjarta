using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    private static SaveData instance = null;
    public static SaveData Instance
    {
        get 
        { 
            if (instance == null)
                instance = new SaveData();
            return instance;
        }
        set { instance = value; }
    }
    private SaveData() { }

    public PlayerData playerData;

    public List<EntityData> enemyData = new List<EntityData>();
}
