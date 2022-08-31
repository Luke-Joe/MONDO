using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject endMenuUI;

    public void OnPauseGame()
    {
        if (!GameManager.gameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
            // Ball still spawnable on pause -> Change how game is paused
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        GameManager.roundEnded = false;
        GameManager.gameIsPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Classic");
        if (GameManager.roundEnded)
        {
            endMenuUI.SetActive(false);
        }
        GameManager.roundEnded = false;
    }

    public void EndGame()
    {
        if (!GameManager.roundEnded)
        {
            Time.timeScale = 0;
            endMenuUI.SetActive(true);
            GameManager.roundEnded = true;
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameManager.gameIsPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.gameIsPaused = false;
    }
}
