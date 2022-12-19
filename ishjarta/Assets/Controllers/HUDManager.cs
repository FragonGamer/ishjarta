using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] Image ActiveItemSprite;
    [SerializeField] Image MeleeWeaponSprite;
    [SerializeField] Image MeleeWeaponSelectedSprite;
    [SerializeField] Image RangedWeaponSprite;
    [SerializeField] Image RangedWeaponSelectedSprite;
    [SerializeField] TMP_Text ArmorValueText;
    [SerializeField] TMP_Text CoinValueText;
    [SerializeField] Inventory playerInventory;
    [SerializeField] Player player;
    private GameObject ItemInfoUI;
    private TMPro.TextMeshProUGUI ItemInfoText;
    private ItemManager nearestItemManager;

    #region Singleton
    public static HUDManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    void Update()
    {
        UpdateItemInfoUI();
    }

    private void Start()
    {
        playerInventory = Inventory.instance;
        player = PlayerManager.instance.player.GetComponent<Player>();
        MeleeWeaponSelectedSprite.gameObject.SetActive(false);
        RangedWeaponSelectedSprite.gameObject.SetActive(false);
        UpdateAllSpritesAndText();
        InitItemInfoUI();
    }
    public void UpdateAllSpritesAndText()
    {
        if (playerInventory.GetActiveItem() != null)
            ActiveItemSprite.sprite = playerInventory.GetActiveItem().GetSprite();

        UpdateWeaponSprites();
        ArmorValueText.text = $"{Mathf.RoundToInt(player.GetResistence() * 100)} %";
        CoinValueText.text = playerInventory.GetCoins().Amount.ToString();
    }
    public void UpdateWeaponSprites()
    {
        if (playerInventory.GetMeleeWeapon() != null)
        {
            MeleeWeaponSprite.gameObject.SetActive(true);
            MeleeWeaponSprite.sprite = playerInventory.GetMeleeWeapon().GetSprite();
        }
        if (playerInventory.GetRangedWeapon() != null)
        {
            RangedWeaponSprite.gameObject.SetActive(true);
            RangedWeaponSprite.sprite = playerInventory.GetRangedWeapon().GetSprite();
        }
        UpdateSelectedWeaponSprites();
    }
    private void InitItemInfoUI(){
        ItemInfoUI = GameObject.FindGameObjectWithTag("HUD").transform.Find("ItemInfoContainer").gameObject;
        ItemInfoText = ItemInfoUI.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        ItemInfoUI.SetActive(false);
        
    }
    private void UpdateItemInfoUI(){
        nearestItemManager = player.nearestItemManager;
        var isItemInRange = nearestItemManager.isInRange ? true : false;
        if(isItemInRange){
            var item = nearestItemManager.GetItem();
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"{item.ItemName}");
            stringBuilder.AppendLine();
            if(nearestItemManager.showFullDescription) stringBuilder.AppendLine($"{item.fullDescription}");
            else stringBuilder.AppendLine($"{item.description}");
            stringBuilder.AppendLine();

            ItemInfoText.text = stringBuilder.ToString();
            ItemInfoUI.SetActive(true);
        }
        else{
             ItemInfoUI.SetActive(false);
        }
    }
    public void UpdateSelectedWeaponSprites()
    {
        if (playerInventory.CurrentWeapon != null)
        {
            if (playerInventory.CurrentWeapon.GetType() == typeof(MeleeWeapon))
            {
                MeleeWeaponSelectedSprite.gameObject.SetActive(true);
                RangedWeaponSelectedSprite.gameObject.SetActive(false);
            }
            else if (playerInventory.CurrentWeapon.GetType() == typeof(RangedWeapon))
            {
                MeleeWeaponSelectedSprite.gameObject.SetActive(false);
                RangedWeaponSelectedSprite.gameObject.SetActive(true);
            }
        }
    }
}
