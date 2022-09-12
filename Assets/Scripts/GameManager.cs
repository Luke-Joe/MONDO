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
    public TMP_Text countdownTimer;

    public GameObject pauseMenuUI;
    public GameObject endMenuUI;
    public GameObject countdownUI;

    private static string p1ScoreKey = "PLAYER1_SCORE";
    private static string p2ScoreKey = "PLAYER2_SCORE";


    private float startingTime = 3.5f;
    private float currTime = 0f;

    public void OnPauseGame()
    {
        if (!gameIsPaused)
        {
            Pause();
        }
        else
        {
            Resume();
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
        StartCoroutine("EndGameCoroutine");
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

    public void Save()
    {
        p1ScoreText.text = p1Score.ToString();
        p2ScoreText.text = p2Score.ToString();
        PlayerPrefs.SetInt(p1ScoreKey, p1Score);
        PlayerPrefs.SetInt(p2ScoreKey, p2Score);
        PlayerPrefs.Save();
    }

    void Start()
    {
        currTime = startingTime;

        if (PlayerPrefs.HasKey(p1ScoreKey))
        {
            p1Score = PlayerPrefs.GetInt(p1ScoreKey);
        }

        if (PlayerPrefs.HasKey(p2ScoreKey))
        {
            p2Score = PlayerPrefs.GetInt(p2ScoreKey);
        }
    }

    void Update()
    {
        currTime -= Time.deltaTime;
        countdownTimer.text = currTime.ToString("0");

        if (currTime < 0.1)
        {
            currTime = 0;
            countdownUI.SetActive(false);
        }
    }

    public IEnumerator EndGameCoroutine()
    {
        yield return new WaitForSeconds(0.6f);

        if (!roundEnded)
        {
            Time.timeScale = 0;
            endMenuUI.SetActive(true);
            roundEnded = true;
        }
    }
}
