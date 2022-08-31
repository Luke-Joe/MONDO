using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool roundEnded = false;
    public static bool gameIsPaused = false;

    public void EndRound()
    {
        if (!roundEnded)
        {
            roundEnded = true;
            NewRound();
            //Display menu with options to restart/exit
        }
    }

    public void NewRound()
    {
        roundEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
