using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed = 5f;
    public Rigidbody2D rb;
    public Joystick joystick;

    public Vector2 direction;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        direction.x = joystick.Horizontal;
        direction.y = joystick.Vertical;
        // movement.Normalize();
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
