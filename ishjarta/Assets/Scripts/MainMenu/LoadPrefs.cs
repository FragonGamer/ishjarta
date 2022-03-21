using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadPrefs : MonoBehaviour
{
    [Header("Generals Setting")]
    [SerializeField] private bool canUse;
    [SerializeField] private MenuController menuController;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;

    [Header("Brightness Setting")]
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private Slider brightnessSlider = null;

    [Header("Quality Level Setting")]
    [SerializeField] private TMP_Dropdown qualityDropdown;

    [Header("Fullscreen Setting")]
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Sensitivity Setting")]
    [SerializeField] private TMP_Text controllerSensitivityTextValue = null;
    [SerializeField] private Slider controllerSensitivitySlider = null;


    private void Awake()
    {
        if(canUse)
        {
            if(PlayerPrefs.HasKey("masterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioListener.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Sound");
            }

            //menuController.ResetButton("Graphics");

            if (PlayerPrefs.HasKey("masterQuality"))
            {
                int localQuality = PlayerPrefs.GetInt("masterQuality");

                qualityDropdown.value = localQuality;
                QualitySettings.SetQualityLevel(localQuality);
            }

            if (PlayerPrefs.HasKey("masterFullScreen"))
            {
                int localFullScreen = PlayerPrefs.GetInt("masterFullScreen");

                if(localFullScreen == 1)
                {
                    Screen.fullScreen = true;
                    fullScreenToggle.isOn = true;
                }
                else
                {
                    Screen.fullScreen = false;
                    fullScreenToggle.isOn = false;
                }
            }

            if (PlayerPrefs.HasKey("masterBrightness"))
            {
                float localBrightness = PlayerPrefs.GetFloat("masterBrightness");

                brightnessTextValue.text = localBrightness.ToString("0.0");
                brightnessSlider.value = localBrightness;
            }

            if (PlayerPrefs.HasKey("masterControllerSensitivity"))
            {
                int localControllerSensitivity = PlayerPrefs.GetInt("masterControllerSensitivity");

                controllerSensitivityTextValue.text = localControllerSensitivity.ToString("0");
                controllerSensitivitySlider.value = localControllerSensitivity;
                menuController.mainControllerSensitivity = localControllerSensitivity;
            }
            else
            {
                menuController.ResetButton("Gameplay");
            }
        }
    }

}
