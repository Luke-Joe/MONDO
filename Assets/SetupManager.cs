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
    }

    void Update()
    {
        if (player1.PlayerReady && player2.PlayerReady)
        {
            gm.setupComplete = true;
            setupUI.SetActive(false);
            countdownUI.SetActive(true);
        }
    }
}
