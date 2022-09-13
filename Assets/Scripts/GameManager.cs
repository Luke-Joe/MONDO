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
    private AudioManager audioManager;

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
        StartCoroutine("LoadMenuCoroutine");
    }

    IEnumerator LoadMenuCoroutine()
    {
        audioManager.Play("UIBlip");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(audioManager.FindDuration("UIBlip"));
        SceneManager.LoadScene("Menu");
        roundEnded = false;
        gameIsPaused = false;
    }

    public void RestartGame()
    {
        StartCoroutine("RestartGameCoroutine");
    }

    IEnumerator RestartGameCoroutine()
    {
        audioManager.Play("UIBlip");
        Time.timeScale = 1f;
        yield return new WaitForSeconds(audioManager.FindDuration("UIBlip"));
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
        audioManager.Play("UIBlip");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    void Resume()
    {
        audioManager.Play("UIBlip");
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
        audioManager = FindObjectOfType<AudioManager>();
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

        if (currTime < 0.25)
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
