using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSound : MonoBehaviour
{
    AudioSource audioSource;
    public List<AudioClip> sounds;

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
        audioSource= GetComponent<AudioSource>();
        audioSource.clip = sounds[0];
        audioSource.Play();
    }
}
