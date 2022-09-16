using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class StartManager : MonoBehaviour
{
    public GameObject helpMenuUI;
    public GameObject settingsUI;
    public string LevelName;

    private AudioManager audioManager;
    private static bool settingsActive = false;
    public static bool helpMenuActive = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.HasKey("SFX_STATUS") && !Convert.ToBoolean(PlayerPrefs.GetInt("SFX_STATUS")))
        {
            audioManager.Mute();
        }
        else
        {
            audioManager.Unmute();
        }
    }

    public void LoadLevel()
    {
        audioManager.Play("UIBlip");
        SceneManager.LoadScene(LevelName);
    }

    public void helpMenu()
    {
        audioManager.Play("UIBlip");

        if (!helpMenuActive)
        {
            helpMenuUI.SetActive(true);
            helpMenuActive = true;
        }
        else
        {
            helpMenuUI.SetActive(false);
            helpMenuActive = false;
        }
    }

    public void settings()
    {
        audioManager.Play("UIBlip");

        if (!settingsActive)
        {
            settingsUI.SetActive(true);
            settingsActive = true;
        }
        else
        {
            settingsUI.SetActive(false);
            settingsActive = false;
        }
    }
}
