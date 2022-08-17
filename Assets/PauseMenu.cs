using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void OnPauseGame() {
        if (!GameManager.gameIsPaused) {
            Pause(); 
        } else {
            Resume();
            // Ball still spawnable on pause -> Change how game is paused
        }
    }

    public void LoadMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Classic");
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameManager.gameIsPaused = true;
    }

    void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.gameIsPaused = false;
    }
}
 