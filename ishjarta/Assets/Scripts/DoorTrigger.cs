using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTrigger : MonoBehaviour
{
    public string levelIndex;
    public string nextSceneString;
    public bool doorIsOpen;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (doorIsOpen && collider.tag == "Player")
            SceneManager.LoadScene($"Scenes/{levelIndex}/{nextSceneString}");
    }
}
