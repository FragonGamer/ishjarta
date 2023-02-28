using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "active items/Gjallarhorn")]

public class Gjallarhorn : ActiveItem
{
    [SerializeField]
    private int damage = 200;

    public override void Activate(GameObject parent)
    {
        var player = parent.GetComponent<Player>();
        var room = player.currentRoom;
        if(room == null)
            return;
        var enemies = room.GetComponent<Room>().Enemies.ToArray();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().ReceiveDamage(damage);
        }
        
    }
}
