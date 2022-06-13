using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Random = System.Random;

public class Itemspawner : MonoBehaviour
{

    private LevelName levelname;
    private List<GameObject> gos = new List<GameObject>();
    
    [field: SerializeField]public int itemAmount { get; set; }

    
    private void Awake()
    {
        levelname = FindObjectOfType<StageController>().currentStageName;
        var Bundle = Utils.loadAssetPack("item");
        var assets = Bundle.LoadAllAssets<GameObject>();
        gos = assets.Where(go => go.GetComponent<ItemManager>().GetItem().SpawnLevelPool.Contains(this.levelname)).ToList();
        
        Bundle.Unload(false);
    }

    public GameObject[] Spawn()
    {
        List<GameObject> items = new List<GameObject>();
        
        for (int i = 0; i < itemAmount; i++)
        {
            items.Add(this.SpawnItem(gos));
        }

        return items.ToArray();
    }
    private GameObject SpawnItem(List<GameObject> items)
    {
        var random = new Random();
        var randVal = random.Next(0, 101);
        GameObject item;

        if (randVal >= RarityRange.CommonRange["Min"] && randVal <= RarityRange.CommonRange["Max"] )
        {
            var itemsNew = items.Where(i => i.GetComponent<ItemManager>().GetItem().Rarity.Equals(Rarity.common));

            Debug.Log(items.First().GetComponent<ItemManager>().GetItem().Rarity);

            itemsNew.ToList().Shuffle();
            itemsNew.ToList().Shuffle();
            item = itemsNew.First();
            var position = this.transform.position;

            return Instantiate(item, position, new Quaternion(0, 0, 0, 0));
        }
        else if (randVal >= RarityRange.UncommonRange["Min"] && randVal <= RarityRange.UncommonRange["Max"])
        {
            var itemsNew = items.Where(i => i.GetComponent<ItemManager>().GetItem().Rarity.Equals(Rarity.uncommon));
            
            itemsNew.ToList().Shuffle();
            itemsNew.ToList().Shuffle();           
            item = itemsNew.First();
            var position = this.transform.position;

            return Instantiate(item, position, new Quaternion(0, 0, 0, 0));
        }
        else if (randVal >= RarityRange.RareRange["Min"] && randVal <= RarityRange.RareRange["Max"])
        {
            var itemsNew = items.Where(i => i.GetComponent<ItemManager>().GetItem().Rarity.Equals(Rarity.rare)) ;
            
            itemsNew.ToList().Shuffle();
            itemsNew.ToList().Shuffle();
            item = itemsNew.First();
            var position = this.transform.position;

            return Instantiate(item, position, new Quaternion(0, 0, 0, 0));
        }
        else if (randVal >= RarityRange.LegendaryRange["Min"] && randVal <= RarityRange.LegendaryRange["Max"])
        {
           var itemsNew = items.Where(i => i.GetComponent<ItemManager>().GetItem().Rarity.Equals(Rarity.legendary));
            
           itemsNew.ToList().Shuffle();
           itemsNew.ToList().Shuffle();
            item = itemsNew.First();
            var position = this.transform.position;

            return Instantiate(item, position, new Quaternion(0, 0, 0, 0));
        }

        return null;
    }
}
