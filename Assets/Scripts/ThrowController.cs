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
    public Joystick throwJoystick;
    public Joystick moveJoystick;
    public GameObject projectile;
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;
    private AttackState state;
    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();

        points = new GameObject[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = moveJoystick.Horizontal;
        direction.y = moveJoystick.Vertical;

        switch (state)
        {
            case AttackState.cooldown:
                animator.SetBool("Throw", false);
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
                animator.SetBool("Charging", false);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(false);
                }
                if (Mathf.Abs(throwJoystick.Horizontal) >= 0.2f || Mathf.Abs(throwJoystick.Vertical) >= 0.2f)
                {
                    state = AttackState.charging;
                }
                break;
            case AttackState.charging:
                // Scale speed/dmg
                playerMovement.speed = 2f;
                animator.SetBool("Charging", true);
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
                if (throwJoystick.Horizontal == 0 && throwJoystick.Vertical == 0)
                {
                    state = AttackState.active;
                }
                break;
            case AttackState.active:
                playerMovement.speed = 5f;
                animator.SetBool("Throw", true);
                animator.SetBool("Charging", false);
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
