using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MenuController : BaseMenuController
{

    [Header("Level")]
    public string newGameLevel = null;
    [SerializeField] private GameObject noSavedGameDialog = null;


    public void NewGameDialogYes()
    {
        new SerializationManager().DeleteSaveFile("system");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadGameDialogYes()
    {
        if (SerializationManager.ExistsSaveFile("system"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else if(noSavedGameDialog != null)
            noSavedGameDialog.SetActive(true);
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
