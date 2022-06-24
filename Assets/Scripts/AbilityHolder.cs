using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour {
    public Ability ability;
    public KeyCode key;
    private float cooldownTime;
    private float activeTime;
    private bool isActive = false;

    private enum AbilityState {
        ready,
        active,
        cooldown
    }

    AbilityState currState = AbilityState.ready;

    // Update is called once per frame
    void Update() {
        switch (currState) {
            case AbilityState.ready:
                if (isActive) {
                    ability.Activate(gameObject);
                    currState = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0) {
                    activeTime -= Time.deltaTime;
                } else {
                    currState = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                    isActive = false;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0) {
                    cooldownTime -= Time.deltaTime;
                } else {
                    currState = AbilityState.ready;
                }
                break;
        }
    }

    public void OnClick() {
        isActive = true;
    }
}
