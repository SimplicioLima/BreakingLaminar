using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class UI_Controller : MonoBehaviour
{
    public GameObject pauseGroup;
    public GameObject optionsMenu;
    public GameObject gameUI;
    public bool isPaused = false;

    public Toggle fullscreenToggle, vsyncToggle;
    public List<ResItem> resolutions = new List<ResItem>();
    int selectedResolution;
    public Text resolutionLabel;
    public AudioMixer theMixer;
    public Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;
    //private Volume m_volume;
    //VolumeProfile volumeProfile;
    public Camera mainCamera;
    public GameObject loadingScreen;

    void Start()
    {
        loadingScreen.gameObject.SetActive(false);
        gameUI.SetActive(true);

        fullscreenToggle.isOn = Screen.fullScreen;

        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
        // VSync settings
        if (QualitySettings.vSyncCount == 0) vsyncToggle.isOn = false; else vsyncToggle.isOn = true;

        bool foundResolution = false;

        //volumeProfile = m_volume.sharedProfile;
        //m_volume.

        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundResolution = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        if (!foundResolution)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;

            UpdateResLabel();
        }

        float volume = 0;
        // we want to send the value that we get out from the MasterVolume to the volume variable
        theMixer.GetFloat("MasterVolume", out volume);
        masterSlider.value = volume;
        theMixer.GetFloat("MusicVolume", out volume);
        musicSlider.value = volume;
        theMixer.GetFloat("SFXVolume", out volume);
        sfxSlider.value = volume;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(selectedResolution.ToString());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseGroup.SetActive(isPaused);

            if (isPaused)
            {
                Time.timeScale = 0;
                AudioListener.pause = true;
                
            }
            else
            {
                Time.timeScale = 1;
                AudioListener.pause = false;
            }

        }

        if(isPaused)
            gameUI.SetActive(false);
        else
            gameUI.SetActive(true);
    }

    public void ResLeft()
    {
        selectedResolution--;

        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();
    }

    public void ResRight()
    {
        selectedResolution++;

        // in this case if its higher than two
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }
    public void ApplyGraphics()
    {
        //Screen.fullScreen = fullscreenToggle.isOn;

        if (vsyncToggle.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenToggle.isOn);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void ContinueBtn()
    {
        
        isPaused = false;

        pauseGroup.SetActive(false);
        
        AudioListener.pause = false;

        Time.timeScale = 1;

        
    }

    public void OpenOptions()
    {
        pauseGroup.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void SetMasterVolume()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVolume", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void SetMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVolume", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SetSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVolume", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void ExitBtn()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinueToHQ()
    {
        loadingScreen.gameObject.SetActive(true);
        SceneManager.LoadScene(8);
    }
}

[System.Serializable]
public class ResItem{

    public int horizontal, vertical;
}
