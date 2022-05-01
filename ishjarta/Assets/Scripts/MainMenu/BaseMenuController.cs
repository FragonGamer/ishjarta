using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseMenuController : MonoBehaviour
{
    [Header("Sound Setting")]
    [SerializeField] protected TMP_Text volumeTextValue = null;
    [SerializeField] protected Slider volumeSlider = null;
    [SerializeField] protected float defaultVolume = 0.5f;

    [Header("Gameplay Setting")]
    [SerializeField] protected TMP_Text controllerSensitivityTextValue = null;
    [SerializeField] protected Slider controllerSensitivitySlider = null;
    [SerializeField] protected int defaultControllerSensitivity = 5;
    public int mainControllerSensitivity = 5;

    [Header("Graphic Setting")]
    [SerializeField] protected TMP_Text brightnessTextValue = null;
    [SerializeField] protected Slider brightnessSlider = null;
    [SerializeField] protected int defaultBrightness = 50;

    [Space(10)]
    [SerializeField] protected TMP_Dropdown qualityDropdown;
    [SerializeField] protected Toggle fullScreenToggle;

    protected int qualityLevel;
    protected bool isFullScreen;
    protected float brightnessLevel;

    [Header("Resolution Dropdowns")]
    [SerializeField] protected TMP_Dropdown resolutionDropdown = null;
    protected Resolution[] resolutions;

    private void Start()
    {
        if(resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                    currentResolutionIndex = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
    }

    public void SetControllerSensitivity(float sensitivity)
    {
        mainControllerSensitivity = Mathf.RoundToInt(sensitivity);
        controllerSensitivityTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetInt("masterControllerSensitivity", mainControllerSensitivity);
    }

    public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0");
    }

    public void SetFullScreen(bool fullScreen)
    {
        isFullScreen = fullScreen;
    }

    public void SetQuality(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);

        PlayerPrefs.SetInt("masterQuality", qualityLevel);

        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", isFullScreen ? 1 : 0);
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ResetButton(string menuType)
    {
        if (menuType == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }

        if (menuType == "Sound")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (menuType == "Gameplay")
        {
            controllerSensitivityTextValue.text = defaultControllerSensitivity.ToString("0");
            controllerSensitivitySlider.value = defaultControllerSensitivity;
            mainControllerSensitivity = defaultControllerSensitivity;
            GameplayApply();
        }
    }
}
