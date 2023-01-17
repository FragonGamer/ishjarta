using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private GameObject player;
    private GameObject t1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        t1 = GetMinimapSpawnRoom();
    }
    private GameObject GetMinimapSpawnRoom(){
        return GameObject.FindGameObjectWithTag("HUD").GetComponentInChildren<Minimap>().gameObject.GetComponentInChildren<Grid>().gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(t1 != null)
            this.transform.position = new Vector3(player.transform.position.x + t1.transform.position.x,player.transform.position.y+t1.transform.position.y,-5);
        else{
            t1 = GetMinimapSpawnRoom();
        }
    }
}
