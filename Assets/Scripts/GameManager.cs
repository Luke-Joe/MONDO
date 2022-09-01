using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool roundEnded = false;
    public static bool gameIsPaused = false;
    public int p1Score = 0;
    public int p2Score = 0;
    public TMP_Text p1ScoreText;
    public TMP_Text p2ScoreText;

    public GameObject pauseMenuUI;
    public GameObject endMenuUI;

    public void OnPauseGame()
    {
        if (!gameIsPaused)
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
        roundEnded = false;
        gameIsPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Classic");
        if (roundEnded)
        {
            endMenuUI.SetActive(false);
        }
        roundEnded = false;
    }

    public void EndGame()
    {
        if (!roundEnded)
        {
            Time.timeScale = 0;
            endMenuUI.SetActive(true);
            roundEnded = true;
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    void Update()
    {
        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();
        Debug.Log("P1Score: " + p1Score);
        Debug.Log("PS2Score: " + p2Score);
    }

}
