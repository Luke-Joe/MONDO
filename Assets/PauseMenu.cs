using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public void OnPause() {
        if (!GameManager.gameIsPaused) {
            Time.timeScale = 0;
            GameManager.gameIsPaused = true;
        } else {
            Time.timeScale = 1;
            GameManager.gameIsPaused = false;
            // Ball still spawnable on pause -> Change how game is paused
        }
    }
}
 