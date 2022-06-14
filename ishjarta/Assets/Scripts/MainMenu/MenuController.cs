using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MenuController : BaseMenuController
{

    //[Header("Level")]
    //public string newGameLevel = null;
    [SerializeField] private GameObject noSavedGameDialog = null;

    [Header("LoadBar")]
    [SerializeField] private GameObject loadScreen = null;
    [SerializeField] private Slider loadSlider = null;
    [SerializeField] private TMP_Text loadText = null;


    public void NewGameDialogYes()
    {
        new SerializationManager().DeleteSaveFile("system");
        LoadScene();
    }

    public void LoadGameDialogYes()
    {
        if (SerializationManager.ExistsSaveFile("system"))
            LoadScene();
        else if(noSavedGameDialog != null)
            noSavedGameDialog.SetActive(true);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        loadScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadSlider.value = progress;
            loadText.text = $"{progress * 100f} %";

            yield return null;
        }
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
