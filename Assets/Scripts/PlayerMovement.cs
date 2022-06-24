using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    public Rigidbody2D rb;
    public Joystick joystick;

    private Vector2 movement;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        // movement.Normalize();
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }
}
