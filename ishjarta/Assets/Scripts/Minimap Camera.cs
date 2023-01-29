using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
   [SerializeField] private bool isStatic; 
    private Vector3 position;
    private GameObject player;
    private GameObject t1;
    // Start is called before the first frame update

    public void SetStatic(bool isStatic, Vector3 position){
        if(isStatic){
                this.position = position;
                this.isStatic = true;
            }
            else{
                isStatic = false;
            }
    }
        public void SetStatic(bool isStatic){
            
            if(isStatic){
                if(t1 == null)
                    t1 = GetMinimapSpawnRoom();
                this.position = t1.transform.position - new Vector3(0,0,5);
                this.isStatic = true;
            }
            else{
                this.isStatic = false;
            }
        }

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
        if(isStatic){
           if(this.transform.position != this.position)
                this.transform.position =  this.position;
        }
        else{
            if(t1 != null)
            this.transform.position = new Vector3(player.transform.position.x + t1.transform.position.x,player.transform.position.y+t1.transform.position.y,-5);
        else{
            t1 = GetMinimapSpawnRoom();
        }
        }
        
    }
}
