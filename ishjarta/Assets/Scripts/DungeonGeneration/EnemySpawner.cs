using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{

    AssetBundle enemyAssets;
    GameObject[] possibleEnemies;
    StageController stageController;
    float delay = 3;
    float amount = 5;
    float counter;
    [SerializeField] public float time;
    void Start()
    {
        stageController = FindObjectOfType<StageController>().GetComponent<StageController>();
        enemyAssets = stageController.enemyAssets;
        possibleEnemies = Utils.LoadAllAssetsOfAssetPack(enemyAssets);
    }


    void Update()
    {
        time += Time.deltaTime;
        if (time >= delay && counter < amount)
        {
            time = 0;
            SpawnEnemy();
            counter++;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    void SpawnEnemy()
    {
        var enemy = possibleEnemies.Shuffle().First();
        Instantiate(enemy, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
    }

}
