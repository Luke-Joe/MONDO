using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int hp = 5;
    public int maxHP;
    public bool isDead = false;

    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManagerTag").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        maxHP = hp;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            isDead = true;
            Destroy(gameObject);

            if (gameObject.name == "Player 1")
            {
                gm.p2Score++;
            }
            else
            {
                gm.p1Score++;
            }
            gm.Save();
            gm.EndGame();
        }
    }
}
