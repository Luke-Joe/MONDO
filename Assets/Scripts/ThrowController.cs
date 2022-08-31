using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackState
{
    cooldown,
    ready,
    charging,
    active
}

public class ThrowController : MonoBehaviour
{
    public Transform firepoint;
    public Joystick joystick;
    public GameObject projectile;
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;
    private AttackState state;

    public GameObject pointPrefab;
    public GameObject[] points;
    public int numPoints;

    private Vector2 direction;
    public float shootForce = 1;
    private float maxShootForce = 30;
    private float chargeSpeed = 10;
    public float cooldownTime = 1;
    private float currCooldown = 0;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        state = AttackState.ready;

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
            case AttackState.cooldown:
                if (currCooldown > 0)
                {
                    currCooldown -= Time.deltaTime;
                }
                else
                {
                    state = AttackState.ready;
                }
                break;
            case AttackState.ready:
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
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(true);
                    points[i].transform.position = TrajectoryPosition(i * 0.02f);
                }
                if (shootForce < maxShootForce)
                {
                    shootForce += Time.deltaTime * chargeSpeed;
                }
                //TODO: Ability to cancel attack
                if (joystick.Horizontal == 0 && joystick.Vertical == 0)
                {
                    state = AttackState.active;
                }
                break;
            case AttackState.active:
                playerMovement.speed = 5f;
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(false);
                }
                Shoot();
                shootForce = 1;
                currCooldown = cooldownTime;
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
        newBullet.GetComponent<Projectile>().speed += shootForce;
        state = AttackState.cooldown;
    }

    Vector2 TrajectoryPosition(float t)
    {
        Vector2 currPointPos = (Vector2)firepoint.transform.position + (direction.normalized * shootForce * t);

        return currPointPos;
    }
}
