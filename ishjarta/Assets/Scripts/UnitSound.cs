using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSound : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] sounds = new AudioClip[1];

    private NavMeshAgent nma;

    bool walkingSoundRunning = false;

    IEnumerator PlayWalkingSound(){
        audioSource.clip = sounds[0];
        audioSource.Play();
        walkingSoundRunning = true;
        yield return new WaitForSeconds(.7f);
    }

    void Start(){
        nma = GetComponent<NavMeshAgent>();
        audioSource.clip = sounds[0];
        audioSource.Play();
    }

    void FixedUpdate(){
        if (walkingSoundRunning == false){
            StartCoroutine(PlayWalkingSound());
            walkingSoundRunning = false;
        }
    }
    
}
