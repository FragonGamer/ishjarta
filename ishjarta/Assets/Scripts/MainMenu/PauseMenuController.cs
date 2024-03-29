using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Controller for the PauseMenu
/// </summary>
public class PauseMenuController : BaseMenuController
{
    [Header("Buttons")]
    [SerializeField] private TMP_Text gameplayButton = null;
    [SerializeField] private TMP_Text soundButton = null;
    [SerializeField] private TMP_Text graphicsButton = null;
    [SerializeField] private GameObject pauseMenuContainer = null;
    [SerializeField] private GameObject HUD = null;
    [SerializeField] private GameObject pauseImageContainer = null;
    [SerializeField] private GameObject individualSettingsContainer = null;
    [SerializeField] private GameObject settingsMenuContainer = null;
    InputMaster inputMaster;
    private void Awake()
    {
        inputMaster = new InputMaster();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }
    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private void Start()
    {
        inputMaster.Game.PauseMenu.performed += PauseMenuAction;
        HUD = GameObject.FindGameObjectWithTag("HUD");
    }
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
        SaveManager.Save();
        SceneManager.LoadScene(mainMenu);
    }


    [Header("PauseMenu")]
    [SerializeField] private GameObject pauseMenu = null;

    public void Resume()
    {
        pauseMenuContainer.SetActive(false);
        pauseImageContainer.SetActive(false);
        individualSettingsContainer.SetActive(false);
        settingsMenuContainer.SetActive(false);
        if (HUD!=null)
        {
            HUD.SetActive(true);
        }
        Time.timeScale = 1f;
    }


    private void PauseMenuAction(InputAction.CallbackContext obj)
    {
        if (pauseMenuContainer != null)
        {
            Time.timeScale = 0f;
            pauseMenuContainer.SetActive(!pauseMenuContainer.activeSelf);
            if (pauseMenuContainer.activeSelf == false)
            {
                Time.timeScale = 1f;
            }
            if (HUD != null && pauseMenuContainer.activeSelf == true)
            {
                HUD.SetActive(false);
            }
            else if(pauseMenuContainer.activeSelf == false)
            {
                HUD.SetActive(true);
            }
            if (pauseImageContainer != null)
            {
                pauseImageContainer.SetActive(!pauseImageContainer.activeSelf);
            }
            if (individualSettingsContainer != null&& individualSettingsContainer.activeSelf==true)
            {
                individualSettingsContainer.SetActive(false);
            }
            if (settingsMenuContainer != null && settingsMenuContainer.activeSelf == true)
            {
                settingsMenuContainer.SetActive(false);
            }
        }
    }
}
