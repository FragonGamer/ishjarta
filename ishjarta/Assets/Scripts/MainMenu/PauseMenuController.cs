using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : BaseMenuController
{
    [Header("Buttons")]
    [SerializeField] private TMP_Text gameplayButton = null;
    [SerializeField] private TMP_Text soundButton = null;
    [SerializeField] private TMP_Text graphicsButton = null;

    public void HighlightOnlyGameplayButton()
    {
        CancelHighlightingOfButton();
        gameplayButton.fontStyle = FontStyles.Bold;
    }
    public void HighlightOnlySoundButton()
    {
        CancelHighlightingOfButton();
        soundButton.fontStyle = FontStyles.Bold;
    }
    public void HighlightOnlyGraphicsButton()
    {
        CancelHighlightingOfButton();
        graphicsButton.fontStyle = FontStyles.Bold;
    }

    public void CancelHighlightingOfButton()
    {
        gameplayButton.fontStyle = FontStyles.Normal;
        soundButton.fontStyle = FontStyles.Normal;
        graphicsButton.fontStyle = FontStyles.Normal;
    }


    private string mainMenu = "MainMenu";
    public void QuitButton()
    {
        SceneManager.LoadScene(mainMenu);
    }

}
