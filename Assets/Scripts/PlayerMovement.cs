using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Joystick joystick;

    public Vector2 direction;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = joystick.Horizontal;
        direction.y = joystick.Vertical;
        direction.Normalize();
        animator.SetFloat("Speed", Math.Abs(direction.x) + Math.Abs(direction.y));
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
