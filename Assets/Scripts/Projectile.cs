using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int bounces;

    private Animator animator;
    private SpriteRenderer sr;
    private BoxCollider2D col;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.velocity = transform.up * speed;
        bounces = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player") && rb.velocity.magnitude > 0f && bounces == 0 && !isDead)
        {
            bounces++;
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            collision.gameObject.GetComponentInChildren<DamageController>().Flash();
            Destroy(gameObject);
        }
        else
        {
            bounces++;
            isDead = true;
            animator.SetBool("Spinning", false);
            gameObject.layer = LayerMask.NameToLayer("Noninteractive");
            if (Random.Range(1, 3) < 2)
            {
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }

    void Update()
    {
        if (rb.velocity.magnitude < 1.5f && !isDead)
        {
            animator.SetBool("Spinning", false);
            isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Noninteractive");
            if (Random.Range(1, 3) < 2)
            {
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
    }
}
