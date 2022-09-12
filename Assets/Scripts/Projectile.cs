using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public int bounces;
    public float fadeSpeed;
    public float fadePause;

    private Animator animator;
    private SpriteRenderer sr;
    private BoxCollider2D col;
    private bool isDead = false;
    private bool isFading = false;
    private AudioManager audioManager;
    private ParticleSystem cleaverCollidePS;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
        cleaverCollidePS = GameObject.Find("CleaverCollidePS").GetComponent<ParticleSystem>();
        audioManager.Play("CleaverShoot");
        rb.velocity = transform.up * speed;
        bounces = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        DamageController dc = collision.gameObject.GetComponentInChildren<DamageController>();

        if (collision.gameObject.CompareTag("Player") && rb.velocity.magnitude > 0f && bounces == 0 && !isDead)
        {
            bounces++;
            collision.gameObject.GetComponent<Health>().TakeDamage(1);
            audioManager.StopPlaying("CleaverShoot");
            audioManager.Play("HitSound");
            dc.Flash();
            dc.HitEffects();
            dc.CamShake();
            Destroy(gameObject);
        }
        else
        {
            bounces++;
            audioManager.StopPlaying("CleaverShoot");
            audioManager.Play("HitOther");
            if (!isDead && collision.gameObject.CompareTag("Projectile"))
            {
                cleaverCollidePS.transform.position = collision.gameObject.transform.position;
                cleaverCollidePS.Play();
            }
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
            audioManager.StopPlaying("CleaverShoot");
            audioManager.Play("CleaverFall");
            animator.SetBool("Spinning", false);
            isDead = true;
            gameObject.layer = LayerMask.NameToLayer("Noninteractive");
            if (Random.Range(1, 3) < 2)
            {
                gameObject.transform.Rotate(new Vector3(0, 180, 0));
            }
        }

        if (isDead && isFading == false)
        {
            isFading = true;
            StartFading();
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(5.0f);

        Color objectColor = sr.material.color;
        float alphaValue = sr.material.color.a;

        while (sr.material.color.a > 0f)
        {
            alphaValue -= Time.deltaTime / fadeSpeed;
            sr.material.color = new Color(objectColor.r, objectColor.g, objectColor.b, alphaValue);
            yield return new WaitForSeconds(fadePause);
        }

        sr.material.color = new Color(objectColor.r, objectColor.g, objectColor.b, 0);
        Destroy(gameObject);
    }

    public void StartFading()
    {
        StartCoroutine("FadeOut");
    }
}
