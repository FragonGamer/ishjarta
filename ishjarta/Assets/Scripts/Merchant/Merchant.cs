using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class Merchant : MonoBehaviour
{
    private Room Room { get; set; }
    private Vector3 midPoint;
    [SerializeField] public bool isSix;
    void Start()
    {
        Room = gameObject.GetComponentInParent<Room>();
        midPoint = Room.transform.position + new Vector3(Room.lenX / 2, -Room.lenY / 2);
        var assets = Utils.LoadAssetsFromAddressablesByPath<AssetReference>("Prefabs");
        var itemspawner = Utils.LoadGameObjectFromAddressablesByReferenceWithName(assets, "Itemspawner");
        var itemPriceTag = Utils.LoadGameObjectFromAddressablesByReferenceWithName(assets, "TextObject");
        Utils.UnloadAssetReferences(assets);
        List<GameObject> spawners = new List<GameObject>();
        if (isSix)
        {
            var pos = midPoint - new Vector3(2, -0.5f, 0);
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 3; i++)
                {
                    var go = this.Spawn(itemspawner,pos,itemPriceTag);
                    spawners.Add(go);
                    pos += new Vector3(2, 0, 0);
                }
                pos = midPoint - new Vector3(2, 0.5f, 0);
            }
        }
        else
        {
            var pos = midPoint - new Vector3(2, 0, 0);
            for (int i = 0; i < 3; i++)
            {
                var go = this.Spawn(itemspawner,pos,itemPriceTag);
                spawners.Add(go);
                pos += new Vector3(2, 0, 0);
            }
        }

        
    }

    public bool BuyItem(Player player,Item item)
    {
        if (player.inventory.GetCoins().Amount >= item.Price)
        {
            var coins = Utils.GetCoinObject();
            coins.Amount = item.Price;
            player.inventory.DropItem(coins);
            return true;
        }

        return false;
    }

    GameObject Spawn(GameObject itemspawner,Vector3 pos, GameObject priceTag)
    {
        var go = Instantiate(itemspawner,pos,new Quaternion(0,0,0,0));
        var itemsp = go.GetComponent<Itemspawner>();
        itemsp.itemAmount = 1;
        var items = itemsp.Spawn();
        go.transform.SetParent(Room.gameObject.transform);
        foreach (var item in items)
        {
            item.gameObject.transform.SetParent(go.transform);
        }
        SetPriceOfItems(items,priceTag);
        
        return go;
    }

    void SetPriceOfItems(GameObject[] items,GameObject priceTag)
    {
        
        foreach (var item in items)
        {
            var price = Instantiate(priceTag,item.transform.position + new Vector3(0,-0.5f),new Quaternion(0,0,0,0));
            var text = price.GetComponentInChildren<TextMesh>();
             text.text = item.GetComponent<ItemManager>().GetItem().Price.ToString();
             text.fontSize = 20;
             text.characterSize = 0.07f;
            price.transform.parent = item.gameObject.transform;
        }
        
    }

}
