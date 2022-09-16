using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour
{
    private AudioManager am;

    private bool sfxActive = true;
    private bool controlInversed = false;
    private bool musicActive = true;

    private string SFXPP = "SFX_STATUS";
    private string musicPP = "MUSIC_STATUS";
    private string controlPP = "CONTROL_STATUS";

    public Sprite soundOn;
    public Sprite soundOff;

    public Sprite inverseOn;
    public Sprite inverseOff;

    public Image SFXToggle;
    public Image musicToggle;
    public Image controlToggle;

    void Start()
    {
        am = FindObjectOfType<AudioManager>();

        if (PlayerPrefs.HasKey(SFXPP))
        {
            sfxActive = Convert.ToBoolean(PlayerPrefs.GetInt(SFXPP));

            if (sfxActive)
            {
                SFXToggle.sprite = soundOn;
            }
            else
            {
                SFXToggle.sprite = soundOff;
            }
        }

        if (PlayerPrefs.HasKey(musicPP))
        {
            musicActive = Convert.ToBoolean(PlayerPrefs.GetInt(musicPP));

            if (musicActive)
            {
                musicToggle.sprite = soundOn;
            }
            else
            {
                musicToggle.sprite = soundOff;
            }
        }

        if (PlayerPrefs.HasKey(controlPP))
        {
            controlInversed = Convert.ToBoolean(PlayerPrefs.GetInt(controlPP));

            if (controlInversed)
            {
                controlToggle.sprite = inverseOn;
            }
            else
            {
                controlToggle.sprite = inverseOff;
            }
        }
    }

    public void SetSFX()
    {
        if (sfxActive)
        {
            SFXToggle.sprite = soundOff;
            sfxActive = false;
            PlayerPrefs.SetInt(SFXPP, 0);
            am.Mute();
        }
        else
        {
            am.Unmute();
            am.Play("UIBlip");
            SFXToggle.sprite = soundOn;
            sfxActive = true;
            PlayerPrefs.SetInt(SFXPP, 1);
        }
    }

    public void SetMusic()
    {
        if (musicActive)
        {
            am.Play("UICancel");
            am.MuteTheme();
            am.StopPlaying("Theme");
            musicToggle.sprite = soundOff;
            musicActive = false;
            PlayerPrefs.SetInt(musicPP, 0);
        }
        else
        {
            am.Play("UIBlip");
            am.UnmuteTheme();
            am.Play("Theme");
            musicToggle.sprite = soundOn;
            musicActive = true;
            PlayerPrefs.SetInt(musicPP, 1);
        }
    }

    public void SetControl()
    {
        if (controlInversed)
        {
            am.Play("UICancel");
            controlInversed = false;
            controlToggle.sprite = inverseOff;
            PlayerPrefs.SetInt(controlPP, 0);
        }
        else
        {
            am.Play("UIBlip");
            controlInversed = true;
            controlToggle.sprite = inverseOn;
            PlayerPrefs.SetInt(controlPP, 1);
        }
    }
}
