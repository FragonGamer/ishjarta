// Uhrsprüngliche Klasse für das was jetzt in Inventory.cs unter dem Comment bei Zeile 266 steht

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemHolder : Inventory
{
    public ActiveItem activeItem;
    float cooldownTime;
    float activeTime;

    enum ItemState
    {
        ready,
        active,
        cooldown
    }
    ItemState state = ItemState.ready;

    public KeyCode key;

    void Update()
    {
        switch (state)
        {
            case ItemState.ready:
                if (Input.GetKeyDown(key))
                {
                    activeItem.Activate(gameObject);
                    state = ItemState.active;
                    activeTime = activeItem.activeTime;
                }
                break;
            case ItemState.active:
                if (activeTime > 0)
                    activeTime -= Time.deltaTime;
                else
                {
                    state = ItemState.cooldown;
                    cooldownTime = activeItem.cooldownTime;
                }
                break;
            case ItemState.cooldown:
                if (cooldownTime > 0)
                    cooldownTime -= Time.deltaTime;
                else
                    state = ItemState.active;
                break;
            default:
                break;
        }
    }
}