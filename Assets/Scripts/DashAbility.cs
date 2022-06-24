using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability {
    public float dashForce;

    public override void Activate(GameObject parent) {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();

        rb.AddForce(movement.direction * dashForce);
        Debug.Log("DASH!");
    }
}
