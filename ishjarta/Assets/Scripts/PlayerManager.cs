using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Player player;
    private void Awake()
    {
        player = (Player)GameObject.FindWithTag("Player").GetComponent(typeof(Player));
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            player.Attack(Input.mousePosition);
        }
    }

}
