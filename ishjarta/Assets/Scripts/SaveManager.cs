using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static string fileName = "fail";

    public static string FileName => fileName;

    void Start()
    {
        fileName = SceneManager.GetActiveScene().name;
        Debug.Log(fileName);

        bool loaded;
        SaveData.Instance = (SaveData)SerializationManager.Load(fileName, out loaded);
        if (loaded)
        {
            Debug.Log("File loaded");

            PlayerData playerData = SaveData.Instance.playerData;
            Player player = PlayerManager.instance.player.GetComponent<Player>();

            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                if (o.GetComponent<Enemy>() != null)
                {
                    Destroy(o);
                }
                else if (o.GetComponent<ItemManager>() != null)
                {
                    Destroy(o);
                }
                else if(o.GetComponent<Room>() != null)
                {
                    if(playerData.roomId < 0)
                    {
                        var room = o.GetComponent<Room>();
                        if (room.RoomId == playerData.roomId)
                        {
                            o.SetActive(true);
                            player.currentRoom = room;
                        }
                        else
                            o.SetActive(false);
                    }
                }
            }


            player.Init(playerData);

            var enemyData = SaveData.Instance.enemyData;

            var enemyBundle = Utils.loadAssetPack("enemies/forest");

            foreach (var ed in enemyData)
            {
                if (ed.enemyType == (int)Enemy.EnemyEnum.slime)
                {
                    var slime = Utils.loadAssetFromAssetPack(enemyBundle, "slime");
                    slime.GetComponent<Enemy>().Init(ed);
                    Instantiate(slime, ed.position, Quaternion.identity);
                }
                else if (ed.enemyType == (int)Enemy.EnemyEnum.rangedSlime)
                {
                    var rangedSlime = Utils.loadAssetFromAssetPack(enemyBundle, "RangedSlime");
                    rangedSlime.GetComponent<Enemy>().Init(ed);
                    Instantiate(rangedSlime, ed.position, Quaternion.identity);
                }
            }
            Utils.UnloadAssetPack(enemyBundle);


            string asset = string.Empty;

            var meleeWeaponData = SaveData.Instance.meleeWeaponData;

            var meleeWeaponPrefabBundle = Utils.loadAssetPack("meleeweaponprefab");
            var meleeWeaponItemBundle = Utils.loadAssetPack("meleeweapon");

            foreach (var mwd in meleeWeaponData)
            {
                //if (mwd.weaponType == (int)MeleeWeapon.MeleeWeaponType.sword)
                //{
                //    var sword = Utils.loadAssetFromAssetPack(meleeWeaponBundle, "sword");

                //    //var meleeWeapon = ScriptableObject.CreateInstance<MeleeWeapon>();
                //    //meleeWeapon.Init(mw);
                //    //sword.GetComponent<ItemManager>().SetItem(meleeWeapon);

                //    Instantiate(sword, mwd.position, Quaternion.identity);
                //}
                //else if (mwd.weaponType == (int)MeleeWeapon.MeleeWeaponType.redSword)
                //{
                //    var redsword = Utils.loadAssetFromAssetPack(meleeWeaponBundle, "redsword");

                //    //var meleeWeapon = ScriptableObject.CreateInstance<MeleeWeapon>();
                //    //meleeWeapon.Init(mw);
                //    //redsword.GetComponent<ItemManager>().SetItem(meleeWeapon);

                //    Instantiate(redsword, mwd.position, Quaternion.identity);
                //}

                asset =
                    mwd.weaponType == (int)MeleeWeapon.MeleeWeaponType.sword ? "sword" :
                    mwd.weaponType == (int)MeleeWeapon.MeleeWeaponType.redSword ? "redsword" :
                    "";
                if(asset != string.Empty)
                {
                    var meleeWeaponPrefab = Utils.loadAssetFromAssetPack(meleeWeaponPrefabBundle, asset);
                    var meleeWeaponItem = Utils.loadItemFromAssetPack(meleeWeaponItemBundle, mwd.itemName);
                    meleeWeaponPrefab.GetComponent<ItemManager>().SetItem(meleeWeaponItem);

                    Instantiate(meleeWeaponPrefab, mwd.position, Quaternion.identity);
                }
            }
            Utils.UnloadAssetPack(meleeWeaponPrefabBundle);
            Utils.UnloadAssetPack(meleeWeaponItemBundle);


            var rangedWeaponData = SaveData.Instance.rangedWeaponData;

            var rangedWeaponPrefabBundle = Utils.loadAssetPack("rangedweaponprefab");
            var rangedWeaponItemBundle = Utils.loadAssetPack("rangedweapon");

            foreach (var rwd in rangedWeaponData)
            {
                //if (rw.weaponType == (int)RangedWeapon.RangedWeaponType.bow)
                //{
                //    var bow = Utils.loadAssetFromAssetPack(rangedWeaponBundle, "bow");

                //    //var rangedWeapon = ScriptableObject.CreateInstance<RangedWeapon>();
                //    //rangedWeapon.Init(rw);
                //    //bow.GetComponent<ItemManager>().SetItem(rangedWeapon);

                //    Instantiate(bow, rw.position, Quaternion.identity);
                //}
                asset =
                   rwd.weaponType == (int)RangedWeapon.RangedWeaponType.bow ? "bow" :
                   "";
                if(asset != string.Empty)
                {
                    var rangedWeaponPrefab = Utils.loadAssetFromAssetPack(rangedWeaponPrefabBundle, asset);
                    var rangedWeaponItem = Utils.loadItemFromAssetPack(rangedWeaponItemBundle, rwd.itemName);
                    rangedWeaponPrefab.GetComponent<ItemManager>().SetItem(rangedWeaponItem);

                    Instantiate(rangedWeaponPrefab, rwd.position, Quaternion.identity);
                }
            }
            Utils.UnloadAssetPack(rangedWeaponPrefabBundle);
            Utils.UnloadAssetPack(rangedWeaponItemBundle);


            var activeItemData = SaveData.Instance.activeItemData;

            var activeItemPrefabBundle = Utils.loadAssetPack("activeitemprefab");
            var activeItemBundle = Utils.loadAssetPack("activeitem");

            foreach (var aid in activeItemData)
            {
                //if (ai.activeItemType == (int)ActiveItem.ActiveItemtype.speedBraclet)
                //{
                //    var braclet = Utils.loadAssetFromAssetPack(activeItemBundle, "speedbraclet");

                //    //var activeItem = ScriptableObject.CreateInstance<ActiveItem>();
                //    //activeItem.Init(ai);
                //    //braclet.GetComponent<ItemManager>().SetItem(activeItem);

                //    Instantiate(braclet, ai.position, Quaternion.identity);
                //}
                asset =
                    aid.activeItemType == (int)ActiveItem.ActiveItemtype.speedBraclet ? "speedbraclet" :
                    "";
                if(asset != string.Empty)
                {
                    var activeItemPrefab = Utils.loadAssetFromAssetPack(activeItemPrefabBundle, asset);
                    var activeItem = Utils.loadItemFromAssetPack(activeItemBundle, aid.itemName);
                    activeItemPrefab.GetComponent<ItemManager>().SetItem(activeItem);

                    Instantiate(activeItemPrefab, aid.position, Quaternion.identity);
                }
            }
            Utils.UnloadAssetPack(activeItemPrefabBundle);
            Utils.UnloadAssetPack(activeItemBundle);


            var passivItemData = SaveData.Instance.passivItemData;

            var passivItemPrefabBundle = Utils.loadAssetPack("passivitemprefab");
            var passivItemBundle = Utils.loadAssetPack("passivitem");

            foreach (var pid in passivItemData)
            {
                //if (pi.passivItemType == (int)PassiveItem.PassivItemtype.flower)
                //{
                //    var flower = Utils.loadAssetFromAssetPack(passivItemBundle, "baseflower");

                //    //var passivItem = ScriptableObject.CreateInstance<PassiveItem>();
                //    //passivItem.Init(pi);
                //    //flower.GetComponent<ItemManager>().SetItem(passivItem);

                //    Instantiate(flower, pi.position, Quaternion.identity);
                //}
                asset =
                    pid.passivItemType == (int)PassiveItem.PassivItemtype.speedFlower ? "speedflower" :
                    "";
                if(asset != string.Empty)
                {
                    var passivItemPrefab = Utils.loadAssetFromAssetPack(passivItemPrefabBundle, asset);
                    var passivItem = Utils.loadItemFromAssetPack(passivItemBundle, pid.itemName);
                    passivItemPrefab.GetComponent<ItemManager>().SetItem(passivItem);

                    Instantiate(passivItemPrefab, pid.position, Quaternion.identity);
                }
            }
            Utils.UnloadAssetPack(passivItemPrefabBundle);
            Utils.UnloadAssetPack(passivItemBundle);


            var usableItemData = SaveData.Instance.usableItemData;

            var usableItemPrefabBundle = Utils.loadAssetPack("usableitemprefab");
            var usableItemBundle = Utils.loadAssetPack("usableitem");

            foreach (var uid in usableItemData)
            {
                //if (ui.usabelItemType == (int)UsableItem.UsableItemtype.coin)
                //{
                //    var coin = Utils.loadAssetFromAssetPack(usableItemBundle, "coin");

                //    //var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                //    //usableItem.Init(ui);
                //    //coin.GetComponent<ItemManager>().SetItem(usableItem);

                //    Instantiate(coin, ui.position, Quaternion.identity);
                //}
                //else if (ui.usabelItemType == (int)UsableItem.UsableItemtype.key)
                //{
                //    var key = Utils.loadAssetFromAssetPack(usableItemBundle, "key");

                //    //var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                //    //usableItem.Init(ui);
                //    //key.GetComponent<ItemManager>().SetItem(usableItem);

                //    Instantiate(key, ui.position, Quaternion.identity);
                //}
                //else if (ui.usabelItemType == (int)UsableItem.UsableItemtype.bomb)
                //{
                //    var bomb = Utils.loadAssetFromAssetPack(usableItemBundle, "bomb");

                //    //var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                //    //usableItem.Init(ui);
                //    //bomb.GetComponent<ItemManager>().SetItem(usableItem);

                //    Instantiate(bomb, ui.position, Quaternion.identity);
                //}
                //else if (ui.usabelItemType == (int)UsableItem.UsableItemtype.armor)
                //{
                //    var armor = Utils.loadAssetFromAssetPack(usableItemBundle, "armor");

                //    //var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                //    //usableItem.Init(ui);
                //    //armor.GetComponent<ItemManager>().SetItem(usableItem);

                //    Instantiate(armor, ui.position, Quaternion.identity);
                //}
                asset =
                    uid.usabelItemType == (int)UsableItem.UsableItemtype.coin ? "coin" :
                    uid.usabelItemType == (int)UsableItem.UsableItemtype.key ? "key" :
                    uid.usabelItemType == (int)UsableItem.UsableItemtype.bomb ? "bomb" :
                    uid.usabelItemType == (int)UsableItem.UsableItemtype.armor ? "armor" :
                    "";
                if(asset != string.Empty)
                {
                    var usabelItemPrefab = Utils.loadAssetFromAssetPack(usableItemPrefabBundle, asset);
                    var usabelItem = Utils.loadItemFromAssetPack(usableItemBundle, uid.itemName);
                    usabelItemPrefab.GetComponent<ItemManager>().SetItem(usabelItem);

                    Instantiate(usabelItemPrefab, uid.position, Quaternion.identity);
                }
            }
            Utils.UnloadAssetPack(usableItemPrefabBundle);
            Utils.UnloadAssetPack(usableItemBundle);

            SaveData.Instance.ClearAll();
        }
        HUDManager.instance.UpdateAllSpritesAndText();
    }

    public void OnApplicationQuit()
    {
        Save();
    }

    public static void Save()
    {
        var objects = GameObject.FindObjectsOfType(typeof(MonoBehaviour));

        for (int i = 0; i < objects.Length; i++)
        {
            var element = objects[i];

            //Debug.Log(element);

            if (element is Player p)
            {
                SaveData.Instance.playerData = new PlayerData(p);
            }
            else if (element is Enemy e)
            {
                SaveData.Instance.enemyData.Add(new EnemyData(e));
            }
            else if (element is ItemManager im)
            {
                Item item = im.GetItem();
                Vector2 position = im.gameObject.transform.position;
                if (item is MeleeWeapon mw)
                {
                    SaveData.Instance.meleeWeaponData.Add(new MeleeWeaponData(mw, position));
                }
                else if (item is RangedWeapon re)
                {
                    SaveData.Instance.rangedWeaponData.Add(new RangedWeaponData(re, position));
                }
                else if (item is ActiveItem ai)
                {
                    SaveData.Instance.activeItemData.Add(new ActiveItemData(ai, position));
                }
                else if (item is PassiveItem pi)
                {
                    SaveData.Instance.passivItemData.Add(new PassivItemData(pi, position));
                }
                else if (item is UsableItem ui)
                {
                    SaveData.Instance.usableItemData.Add(new UsableItemData(ui, position));
                }
            }
        }
        SerializationManager.Save(fileName, SaveData.Instance);
    }

    public static bool DeleteSave(string saveFile)
    {
        return SerializationManager.DeleteSaveFile(saveFile);
    }

    public static bool ExistsSave(string saveFile)
    {
        return SerializationManager.ExistsSaveFile(saveFile);
    }
}
