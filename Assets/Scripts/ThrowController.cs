using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState
{
    empty,
    holding,
    charging,
    attack
}

public class ThrowController : MonoBehaviour
{
    public Transform firepoint;
    public Joystick joystick;
    public GameObject projectile;
    public PlayerMovement playerMovement;

    public GameObject pointPrefab;
    public GameObject[] points;
    public int numPoints;

    private Rigidbody2D rb;
    private AttackState state;

    private Vector2 direction;
    public float power = 1;
    private float maxPower = 30;
    private float chargeSpeed = 10;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        state = AttackState.holding;

        points = new GameObject[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = -joystick.Horizontal;
        direction.y = -joystick.Vertical;

        switch (state)
        {
            case AttackState.holding:
                playerMovement.speed = 5f;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(false);
                }
                if (Mathf.Abs(joystick.Horizontal) >= 0.2f || Mathf.Abs(joystick.Vertical) >= 0.2f)
                {
                    state = AttackState.charging;
                }
                break;
            case AttackState.charging:
                // Scale speed/dmg
                playerMovement.speed = 2f;
                if (power < maxPower)
                {
                    power += Time.deltaTime * chargeSpeed;
                }
                if (joystick.Horizontal == 0 && joystick.Vertical == 0)
                {
                    state = AttackState.attack;
                }
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(true);
                    points[i].transform.position = TrajectoryPosition(i * 0.02f);
                }
                break;
            case AttackState.attack:
                playerMovement.speed = 5f;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(false);
                }
                Shoot();
                power = 0;
                break;
        }
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(projectile, firepoint.position, firepoint.rotation) as GameObject;
        newBullet.GetComponent<Projectile>().speed += power;
        state = AttackState.holding;
    }

    Vector2 TrajectoryPosition(float t)
    {
        Vector2 currPointPos = (Vector2)firepoint.transform.position + (direction.normalized * power * t);

        return currPointPos;
    }
}
