using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PLAYER1_SCORE"))
        {
            PlayerPrefs.DeleteKey("PLAYER1_SCORE");
        }

        if (PlayerPrefs.HasKey("PLAYER2_SCORE"))
        {
            PlayerPrefs.GetInt("PLAYER2_SCORE");
        }
    }
}
