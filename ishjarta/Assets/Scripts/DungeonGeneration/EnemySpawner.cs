using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// <summary>
/// Represents a static object which spawn an certain amount of enemies after an delay
/// </summary>
public class EnemySpawner : MonoBehaviour
{

    AssetBundle enemyAssets;
    GameObject[] possibleEnemies;
    StageController stageController;
    [SerializeField] public float delay = 3;
    [SerializeField] public float amount = 5;
    float counter;
    [SerializeField] public float time;
    Room room;
    Enemy enemy;

    void Start()
    {
        stageController = FindObjectOfType<StageController>().GetComponent<StageController>();
        enemyAssets = stageController.enemyAssets;
        possibleEnemies = Utils.LoadAllAssetsOfAssetPack(enemyAssets);
        room = GetComponentInParent<Room>();
        enemy = this.GetComponent<Enemy>();
        room.SetCleared();

    }


    void Update()
    {
        if (room.isEntered)
        {
            time += Time.deltaTime;
            if (time >= delay && counter < amount)
            {
                time = 0;
                SpawnEnemy();
                counter++;
            }
            if (counter >= amount)
            {
                room.Enemies.Remove(enemy);
                Destroy(this.gameObject);
            }
        }

    }
    void SpawnEnemy()
    {

        var enemy = possibleEnemies.Shuffle().First();

        enemy.GetComponent<Enemy>().enemyLootDropTable = EnemyLootDropTable.GetEnemyLootDropTableOfSlime();

        var go = Instantiate(enemy, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        room.Enemies.Add(go.GetComponent<Enemy>());
        go.transform.parent = room.gameObject.transform;
    }

}
