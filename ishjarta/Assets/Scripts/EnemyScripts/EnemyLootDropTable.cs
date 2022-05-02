using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyLootDropTable : ScriptableObject
{
    [System.Serializable]
    public class LootDrop /*: ScriptableObject*/
    {
        public GameObject drop;
        public int weight;
    }

    [SerializeField] public List<LootDrop> table;

    private void Awake()
    {
        int totalWeight = 0;
        for (int i = 0; i < table.Count; i++)
        {
            totalWeight += table[i].weight;
        }
        TotalWeight = totalWeight;
    }

    public int TotalWeight { get; private set; }

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
}
