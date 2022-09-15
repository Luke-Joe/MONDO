using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Image charViewer;
    public SpriteRenderer player;
    public TMP_Text SetupStatus1;

    public Button NextButton1;
    public Button PrevButton1;

    public int index = 0;
    public bool PlayerReady = false;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void NextOption()
    {
        audioManager.Play("UIBlip");
        index++;

        if (index > characterDB.characterCount() - 1)
        {
            index = 0;
        }

        UpdateCharacter(index);
    }

    public void PrevOption()
    {
        audioManager.Play("UIBlip");
        index--;

        if (index < 0)
        {
            index = characterDB.characterCount() - 1;
        }

        UpdateCharacter(index);
    }

    public void UpdateCharacter(int index)
    {
        Character character = characterDB.GetCharacter(index);
        charViewer.material = character.characterMaterial;
        player.material = characterDB.GetCharacter(index).characterMaterial;
    }

    public void Ready()
    {
        if (!PlayerReady)
        {
            audioManager.Play("UIBlip");
            SetupStatus1.text = "CANCEL";
            PlayerReady = true;
            NextButton1.gameObject.SetActive(false);
            PrevButton1.gameObject.SetActive(false);
        }
        else
        {
            audioManager.Play("UICancel");
            PlayerReady = false;
            SetupStatus1.text = "READY";
            NextButton1.gameObject.SetActive(true);
            PrevButton1.gameObject.SetActive(true);
        }
    }


}
