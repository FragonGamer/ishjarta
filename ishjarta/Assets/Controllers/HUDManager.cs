using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    #region Singleton
    public static HUDManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    private void Start()
    {
        playerInventory = Inventory.instance;
        player = PlayerManager.instance.player.GetComponent<Player>();
        MeleeWeaponSelectedSprite.gameObject.SetActive(false);
        RangedWeaponSelectedSprite.gameObject.SetActive(false);
        UpdateAllSpritesAndText();
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
