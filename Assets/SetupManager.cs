using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
    public CharacterManager player1;
    public CharacterManager player2;

    public GameObject setupUI;
    public GameObject countdownUI;

    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        if (PlayerPrefs.HasKey("P1_SELECTION") && PlayerPrefs.HasKey("P2_SELECTION"))
        {
            player1.UpdateCharacter(PlayerPrefs.GetInt("P1_SELECTION"));
            player2.UpdateCharacter(PlayerPrefs.GetInt("P2_SELECTION"));
        }
    }

    void Update()
    {
        if (player1.PlayerReady && player2.PlayerReady)
        {
            PlayerPrefs.SetInt("SETUP_COMPLETE", 1);
            PlayerPrefs.SetInt("P1_SELECTION", player1.index);
            PlayerPrefs.SetInt("P2_SELECTION", player2.index);
            setupUI.SetActive(false);
            countdownUI.SetActive(true);
        }
    }
}
