using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Player player;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance.player.GetComponent<Player>();

        SaveData.Instance = (SaveData)SerializationManager.Load(SerializationManager.savePath + @"\" + "player" + ".save");
        if (SaveData.Instance != null)
        {
            PlayerData pd = (PlayerData)SaveData.Instance.playerData;
            player.Init(pd.currentHealth, pd.maxHealth, pd.baseHealth, pd.healthModifier,
            pd.resistance, pd.currentResistance, pd.movementSpeed, pd.speedModifier, pd.baseDamage,
            pd.damageModifier, pd.attackRate, pd.range);
            player.transform.position = pd.position;
            

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

    void OnDisable()
    {
        //SaveData.Instance.playerData = new PlayerData(player);
        //Debug.Log("Player");
        //SerializationManager.Save("player", SaveData.Instance);

        var objects = GameObject.FindObjectsOfType(typeof(MonoBehaviour));

        for (int i = 0; i < objects.Length; i++)
        {
            var element = objects[i];

            //Debug.Log(element);

            if (element is Player p)
            {
                SaveData.Instance.playerData = new PlayerData(p);
                Debug.Log("Player");
                SerializationManager.Save("player", SaveData.Instance);

            }
            else if (element is Enemy e)
            {
                //SaveData.Instance.enemyData.Add(new EnemyData(e));
                //SerializationManager.Save("enemy", SaveData.Instance);
            }
        }

    }
}
