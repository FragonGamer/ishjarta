using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu]
public class EnemyLootDropTable : ScriptableObject
{
    [System.Serializable]
    public class LootDrop /*: ScriptableObject*/
    {
        public GameObject drop;
        public int weight;
    }

    [SerializeField] public List<LootDrop> table = new List<LootDrop>();

    private void Awake()
    {
        //int totalWeight = 0;
        //for (int i = 0; i < table.Count; i++)
        //{
        //    totalWeight += table[i].weight;
        //}
        //TotalWeight = totalWeight;
    }

    public int TotalWeight
    {
        get
        {
            int totalWeight = 0;
            for (int i = 0; i < table.Count; i++)
            {
                totalWeight += table[i].weight;
            }
            return totalWeight;
        }
    }

    public GameObject GetDrop()
    {
        int chance = UnityEngine.Random.Range(0, TotalWeight);

        for(int i = 0; i < table.Count; i++)
        {
            chance -= table[i].weight;

            if(chance < 0)
            {
                return table[i].drop;
            }
        }

        return null;
    }

    public static EnemyLootDropTable GetEnemyLootDropTableOfSlime()
    {
        EnemyLootDropTable table = new EnemyLootDropTable();
        var usableItemPrefabBundle = Utils.LoadIRessourceLocations<GameObject>(new string[] { "Item" ,"UsableItem"});

        EnemyLootDropTable.LootDrop lt1 = new EnemyLootDropTable.LootDrop();
        lt1.drop = Utils.LoadGameObjectByName(usableItemPrefabBundle, "Coin");
        lt1.weight = 25;
        EnemyLootDropTable.LootDrop lt2 = new EnemyLootDropTable.LootDrop();
        lt2.drop = Utils.LoadGameObjectByName(usableItemPrefabBundle, "Armor");
        lt2.weight = 25;
        EnemyLootDropTable.LootDrop lt3 = new EnemyLootDropTable.LootDrop();
        lt2.drop = null;
        lt2.weight = 50;
        table.table.Add(lt1);
        table.table.Add(lt2);
        table.table.Add(lt3);


        return table;
    }
}
