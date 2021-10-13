using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] TestSlotPosition testSlotPosition;
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    [SerializeField] private ResolutionOptions[] resolutionOptions;

    public Resolution oneResolution;


    private void Start()
    {
        
        fullscreenToggle.isOn = Screen.fullScreen;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
 

        for (int i = 0; i < resolutionOptions.Length; i++)
        {
            string option = resolutionOptions[i].width + " x " + resolutionOptions[i].height;
            options.Add(option);
            if (resolutionOptions[i].height == Screen.height &&
                resolutionOptions[i].width == Screen.width)
            {
                currentResolutionIndex = i;
            }
           
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetSpeed(float speed)
    {
        testSlotPosition.rotatingSpeed = speed;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
 
        Screen.SetResolution(resolutionOptions[resolutionIndex].width, resolutionOptions[resolutionIndex].height, Screen.fullScreen);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
