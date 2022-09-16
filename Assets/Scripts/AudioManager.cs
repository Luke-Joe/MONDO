using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    private static Boolean themePlaying = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            if (s.name == "Theme")
            {
                s.source.loop = true;
            }
        }

        if (PlayerPrefs.HasKey("MUSIC_STATUS") && Convert.ToBoolean(PlayerPrefs.GetInt("MUSIC_STATUS")) && !themePlaying)
        {
            Play("Theme");
            themePlaying = true;
        }

    }

    public void MuteTheme()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Theme");
        if (s == null)
        {
            Debug.LogWarning("Theme not found");
            return;
        }
        themePlaying = false;
        s.source.volume = 0;
    }

    public void UnmuteTheme()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Theme");
        if (s == null)
        {
            Debug.LogWarning("Theme not found");
            return;
        }
        themePlaying = true;
        s.source.volume = s.volume;
    }

    public void Mute()
    {
        foreach (Sound s in sounds)
        {
            if (s.name != "Theme")
            {
                s.source.volume = 0;
            }
        }
    }

    public void Unmute()
    {
        foreach (Sound s in sounds)
        {
            if (s.name != "Theme")
            {
                s.source.volume = s.volume;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }
        s.source.Play();
    }

    public void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return;
        }

        s.source.Stop();
    }

    public float FindDuration(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found.");
            return 0f;
        }

        return s.clip.length;
    }
}
