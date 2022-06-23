using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState {
    empty,
    holding,
    charging,
    attack
}

public class ThrowController : MonoBehaviour {
    public Transform firepoint;
    public Joystick joystick;
    public GameObject projectile;
    private Rigidbody2D rb;
    private AttackState state;

    private Vector2 direction;
    private Vector2 force;
    public Vector2 minPower;
    public Vector2 maxPower;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        state = AttackState.holding;
    }

    // Update is called once per frame
    void Update() {
        direction.x = -joystick.Horizontal;
        direction.y = -joystick.Vertical;



        if (state == AttackState.holding && Mathf.Abs(joystick.Horizontal) >= 0.2f || Mathf.Abs(joystick.Vertical) >= 0.2f) {
            state = AttackState.charging;
        }

        if (joystick.Horizontal == 0 && joystick.Vertical == 0 && state == AttackState.charging) {
            Shoot();
            state = AttackState.holding;
        }
    }

    void FixedUpdate() {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    void Shoot() {
        Instantiate(projectile, firepoint.position, firepoint.rotation);
    }
}
