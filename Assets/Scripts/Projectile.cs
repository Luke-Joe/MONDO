using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int bounces;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        bounces = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && rb.velocity.magnitude > 0f && bounces == 0)
        {
            bounces++;
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            collision.gameObject.GetComponentInChildren<DamageController>().Flash();
            Destroy(gameObject);
        }
        else
        {
            bounces++;
            animator.SetBool("Spinning", false);
        }
    }

    void Update()
    {
        if (rb.velocity.magnitude < 1.5f)
        {
            animator.SetBool("Spinning", false);
        }
    }
}
