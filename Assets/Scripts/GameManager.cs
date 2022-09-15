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
    public bool setupComplete = false;
    public int p1Score = 0;
    public int p2Score = 0;
    public TMP_Text p1ScoreText;
    public TMP_Text p2ScoreText;
    public TMP_Text countdownTimer;

    public GameObject pauseMenuUI;
    public GameObject endMenuUI;
    public GameObject countdownUI;
    public GameObject setupUI;
    public GameObject p1;
    public GameObject p2;
    private AudioManager audioManager;
    public SetupManager setupManager;

    private static string p1ScoreKey = "PLAYER1_SCORE";
    private static string p2ScoreKey = "PLAYER2_SCORE";
    private static string setupKey = "SETUP_COMPLETE";

    private float startingTime = 3.5f;
    private float currTime = 0f;
    private bool countdownSFXPlayed = false;

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
        gameIsPaused = false;
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
        PlayerPrefs.SetInt(setupKey, 1);
        PlayerPrefs.Save();
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        currTime = startingTime;
        setupManager.ChangeMat();

        if (PlayerPrefs.HasKey(p1ScoreKey))
        {
            p1Score = PlayerPrefs.GetInt(p1ScoreKey);
        }

        if (PlayerPrefs.HasKey(p2ScoreKey))
        {
            p2Score = PlayerPrefs.GetInt(p2ScoreKey);
        }

        if (PlayerPrefs.GetInt(setupKey) != 0)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            countdownUI.SetActive(true);
        }
        else
        {
            setupUI.SetActive(true);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt(setupKey) != 0)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            currTime -= Time.deltaTime;
            string prev = countdownTimer.text;
            countdownTimer.text = currTime.ToString("0");

            if (prev != countdownTimer.text && currTime > 0.5)
            {
                audioManager.Play("countdownSFX");
            }

            if (currTime < 0.5 && !countdownSFXPlayed)
            {
                audioManager.Play("countdownSFXFinal");
                countdownSFXPlayed = true;
                currTime = 0;
                countdownUI.SetActive(false);
            }
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
