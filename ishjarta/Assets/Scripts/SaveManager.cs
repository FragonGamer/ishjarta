using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SaveManager : MonoBehaviour
{
    public Player player;

    private static string fileName = "system";

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        bool loaded;
        SaveData.Instance = (SaveData)SerializationManager.Load(SerializationManager.savePath + @"/" + fileName + ".save", out loaded);
        if (loaded)
        {
            Debug.Log("File loaded");

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
            }

            PlayerData playerData = SaveData.Instance.playerData;

            player = PlayerManager.instance.player.GetComponent<Player>();

            player.Init(playerData);

            foreach(var passivItem in player.inventory.GetPassiveItems())
            {
                player.AddEffectRange(passivItem.OwnerEffects);
            }

            var enemyData = SaveData.Instance.enemyData;

            var enemyBundle = Utils.loadAssetPack("enemy");

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

            var meleeWeaponData = SaveData.Instance.meleeWeaponData;

            var meleeWeaponBundle = Utils.loadAssetPack("meleeweapon");

            foreach (var mw in meleeWeaponData)
            {
                if (mw.weaponType == (int)MeleeWeapon.MeleeWeaponType.sword)
                {
                    var sword = Utils.loadAssetFromAssetPack(meleeWeaponBundle, "sword");

                    var meleeWeapon = ScriptableObject.CreateInstance<MeleeWeapon>();
                    meleeWeapon.Init(mw);
                    sword.GetComponent<ItemManager>().SetItem(meleeWeapon);

                    Instantiate(sword, mw.position, Quaternion.identity);
                }
                else if (mw.weaponType == (int)MeleeWeapon.MeleeWeaponType.redSword)
                {
                    var redsword = Utils.loadAssetFromAssetPack(meleeWeaponBundle, "redsword");

                    var meleeWeapon = ScriptableObject.CreateInstance<MeleeWeapon>();
                    meleeWeapon.Init(mw);
                    redsword.GetComponent<ItemManager>().SetItem(meleeWeapon);

                    Instantiate(redsword, mw.position, Quaternion.identity);
                }
            }


            var rangedWeaponData = SaveData.Instance.rangedWeaponData;

            var rangedWeaponBundle = Utils.loadAssetPack("rangedweapon");

            foreach (var rw in rangedWeaponData)
            {
                if (rw.weaponType == (int)RangedWeapon.RangedWeaponType.bow)
                {
                    var bow = Utils.loadAssetFromAssetPack(rangedWeaponBundle, "bow");

                    var rangedWeapon = ScriptableObject.CreateInstance<RangedWeapon>();
                    rangedWeapon.Init(rw);
                    bow.GetComponent<ItemManager>().SetItem(rangedWeapon);

                    Instantiate(bow, rw.position, Quaternion.identity);
                }
            }


            var activeItemData = SaveData.Instance.activeItemData;

            var activeItemBundle = Utils.loadAssetPack("activeitem");

            foreach (var ai in activeItemData)
            {
                if (ai.activeItemType == (int)ActiveItem.ActiveItemtype.braclet)
                {
                    var braclet = Utils.loadAssetFromAssetPack(activeItemBundle, "basebraclet");

                    var activeItem = ScriptableObject.CreateInstance<ActiveItem>();
                    activeItem.Init(ai);
                    braclet.GetComponent<ItemManager>().SetItem(activeItem);

                    Instantiate(braclet, ai.position, Quaternion.identity);
                }
            }


            var passivItemData = SaveData.Instance.passivItemData;

            var passivItemBundle = Utils.loadAssetPack("passivitem");

            foreach (var pi in passivItemData)
            {
                if (pi.passivItemType == (int)PassiveItem.PassivItemtype.flower)
                {
                    var flower = Utils.loadAssetFromAssetPack(passivItemBundle, "baseflower");

                    var passivItem = ScriptableObject.CreateInstance<PassiveItem>();
                    passivItem.Init(pi);
                    flower.GetComponent<ItemManager>().SetItem(passivItem);

                    Instantiate(flower, pi.position, Quaternion.identity);
                }
            }


            var usableItemData = SaveData.Instance.usableItemData;

            var usableItemBundle = Utils.loadAssetPack("usableitem");

            foreach (var ui in usableItemData)
            {
                if (ui.usabelItemType == (int)UsableItem.UsableItemtype.coin)
                {
                    var coin = Utils.loadAssetFromAssetPack(passivItemBundle, "coin");

                    var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                    usableItem.Init(ui);
                    coin.GetComponent<ItemManager>().SetItem(usableItem);

                    Instantiate(coin, ui.position, Quaternion.identity);
                }
                else if (ui.usabelItemType == (int)UsableItem.UsableItemtype.key)
                {
                    var key = Utils.loadAssetFromAssetPack(passivItemBundle, "key");

                    var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                    usableItem.Init(ui);
                    key.GetComponent<ItemManager>().SetItem(usableItem);

                    Instantiate(key, ui.position, Quaternion.identity);
                }
                else if (ui.usabelItemType == (int)UsableItem.UsableItemtype.bomb)
                {
                    var bomb = Utils.loadAssetFromAssetPack(passivItemBundle, "bomb");

                    var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                    usableItem.Init(ui);
                    bomb.GetComponent<ItemManager>().SetItem(usableItem);

                    Instantiate(bomb, ui.position, Quaternion.identity);
                }
                else if (ui.usabelItemType == (int)UsableItem.UsableItemtype.armor)
                {
                    var armor = Utils.loadAssetFromAssetPack(passivItemBundle, "armor");

                    var usableItem = ScriptableObject.CreateInstance<UsableItem>();
                    usableItem.Init(ui);
                    armor.GetComponent<ItemManager>().SetItem(usableItem);

                    Instantiate(armor, ui.position, Quaternion.identity);
                }
            }

            SaveData.Instance.ClearAll();


            //PlayerData pd = (PlayerData)SaveData.Instance.playerData;
            //player.Init(pd.currentHealth, pd.maxHealth, pd.baseHealth, pd.healthModifier,
            //pd.resistance, pd.currentResistance, pd.movementSpeed, pd.speedModifier, pd.baseDamage,
            //pd.damageModifier, pd.attackRate, pd.range);
            //player.transform.position = pd.position;


            //if(SaveData.Instance.enemyData.Count > 0)
            //{
            //    EnemyData ed = (EnemyData)SaveData.Instance.enemyData[0];

            //    var enemy = GameObject.FindGameObjectsWithTag("Enemy")[0].GetComponent<Enemy>();

            //    enemy.Init(ed.currentHealth, ed.maxHealth, ed.baseHealth, ed.healthModifier,
            //        ed.resistance, ed.currentResistance, ed.movementSpeed, ed.speedModifier, ed.baseDamage,
            //        ed.damageModifier, ed.attackRate, ed.range);
            //    enemy.transform.position = ed.position;
            //}
        }
    }

    public void OnApplicationQuit()
    {
        //SaveData.Instance.playerData = new PlayerData(player);
        //Debug.Log("Player");
        //SerializationManager.Save(fileName, SaveData.Instance);

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
                else if (im.GetItem() is UsableItem ui)
                {
                    SaveData.Instance.usableItemData.Add(new UsableItemData(ui, position));
                }
            }
        }
        SerializationManager.Save(fileName, SaveData.Instance);
    }
}
