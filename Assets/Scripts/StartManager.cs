using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public GameObject helpMenuUI;
    public static bool helpMenuActive = false;
    public string LevelName;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
}
