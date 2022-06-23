using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
    walk,
    attack
}

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    public Rigidbody2D rb;
    public static PlayerState currState;
    public Joystick joystick;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start() {
        currState = PlayerState.walk;
    }

    // Update is called once per frame
    void Update() {
        if (currState == PlayerState.walk) {
            movement.x = joystick.Horizontal;
            movement.y = joystick.Vertical;
            // movement.Normalize();
        }
    }

    void FixedUpdate() {
        if (currState == PlayerState.walk) {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
    }
}
