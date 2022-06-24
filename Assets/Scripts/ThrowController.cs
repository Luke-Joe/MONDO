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
    // public GameObject pointPrefab;
    // public GameObject[] points;
    // public int numPoints;
    private Rigidbody2D rb;
    private AttackState state;

    private Vector2 direction;
    private Vector2 force;
    public Vector2 minPower;
    public Vector2 maxPower;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        state = AttackState.holding;

        // points = new GameObject[numPoints];

        // for (int i = 0; i < numPoints; i++) {
        //     points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        // }
    }

    // Update is called once per frame
    void Update() {
        direction.x = -joystick.Horizontal;
        direction.y = -joystick.Vertical;

        switch (state) {
            case AttackState.holding:
                if (Mathf.Abs(joystick.Horizontal) >= 0.2f || Mathf.Abs(joystick.Vertical) >= 0.2f) {
                    state = AttackState.charging;
                }
                break;
            case AttackState.charging:
                // Scale speed/dmg
                if (joystick.Horizontal == 0 && joystick.Vertical == 0) {
                    state = AttackState.attack;
                }
                break;
            case AttackState.attack:
                Shoot();
                break;
        }
    }

    void FixedUpdate() {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    void Shoot() {
        Instantiate(projectile, firepoint.position, firepoint.rotation);
        state = AttackState.holding;
    }

    // Vector2 TrajectoryPosition(float t) {
    //     Vector2 currPointPos = transform.position 
    // }
}
