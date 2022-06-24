using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player") && rb.velocity.magnitude > 1.0f) {
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            collision.gameObject.GetComponentInChildren<DamageController>().Flash();
        }
    }
}
