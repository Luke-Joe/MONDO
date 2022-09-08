using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public Image abilityImage1;
    public KeyCode key;
    public ParticleSystem flashPS;
    public ParticleSystem flashPS2;
    private float cooldownTime;
    private float activeTime;
    private bool isActive = false;
    private Vector3 preFlashPos;
    private ThrowController tc;

    private enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState currState = AbilityState.ready;

    void Start()
    {
        abilityImage1.fillAmount = 0;
        tc = GetComponent<ThrowController>();
    }
    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case AbilityState.ready:
                if (isActive && tc.isMoving)
                {
                    preFlashPos = transform.position;
                    flashPS.transform.position = preFlashPos;
                    flashPS.Play();
                    flashPS2.Play();
                    ability.Activate(gameObject);
                    currState = AbilityState.active;
                    activeTime = ability.activeTime;
                    abilityImage1.fillAmount = 0;
                }
                else
                {
                    isActive = false;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    currState = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                    abilityImage1.fillAmount = 1;
                    isActive = false;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                    abilityImage1.fillAmount -= (Time.deltaTime / ability.cooldownTime);

                }
                else
                {
                    currState = AbilityState.ready;
                }
                break;
        }
    }

    public void OnClick()
    {
        isActive = true;
    }
}
