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

    public List<RoomData> roomData = new List<RoomData>();

    //public List<EnemyData> enemyData = new List<EnemyData>();

    //public List<MeleeWeaponData> meleeWeaponData = new List<MeleeWeaponData>();

    //public List<RangedWeaponData> rangedWeaponData = new List<RangedWeaponData>();

    //public List<ActiveItemData> activeItemData = new List<ActiveItemData>();

    //public List<PassivItemData> passivItemData = new List<PassivItemData>();

    //public List<UsableItemData> usableItemData = new List<UsableItemData>();

    public void ClearAll()
    {
        //enemyData = new List<EnemyData>();

        //meleeWeaponData = new List<MeleeWeaponData>();

        //rangedWeaponData = new List<RangedWeaponData>();

        //activeItemData = new List<ActiveItemData>();

        //passivItemData = new List<PassivItemData>();

        //usableItemData = new List<UsableItemData>();

        instance = null;
    }
}
