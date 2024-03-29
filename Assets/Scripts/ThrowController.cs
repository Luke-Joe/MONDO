using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public Transform trajectoryPoint;
    public Joystick throwJoystick;
    public Joystick moveJoystick;
    public GameObject projectile;
    public PlayerMovement playerMovement;

    private Rigidbody2D rb;
    private AttackState state;
    private Animator animator;
    private Health health;

    public GameObject pointPrefab;
    public GameObject[] points;
    public int numPoints;

    private Vector2 direction;
    public float shootForce = 1;
    private float maxShootForce = 30;
    private float chargeSpeed = 10;
    public float cooldownTime = 1;
    private float currCooldown = 0;
    private Vector2 throwDirection;
    private AudioManager audioManager;
    public bool isMoving = false;
    private bool initialInput = false;
    private bool maxCharge = false;
    private bool initCharge = false;


    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
        state = AttackState.ready;
        animator = GetComponentInChildren<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        points = new GameObject[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveJoystick.Horizontal != 0 || moveJoystick.Vertical != 0)
        {
            initialInput = true;
            isMoving = true;
            direction.x = moveJoystick.Horizontal;
            direction.y = moveJoystick.Vertical;
        }
        else
        {
            isMoving = false;
        }


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
                playerMovement.speed = 3f;
                animator.SetBool("Charging", false);
                for (int i = 0; i < points.Length; i++)
                {
                    points[i].SetActive(false);
                }
                if (Mathf.Abs(throwJoystick.Horizontal) >= 0.2f || Mathf.Abs(throwJoystick.Vertical) >= 0.2f)
                {
                    initialInput = true;
                    state = AttackState.charging;
                }
                break;
            case AttackState.charging:
                // Scale speed/dmg
                if (throwJoystick.Horizontal != 0 || throwJoystick.Vertical != 0)
                {
                    direction.x = -throwJoystick.Horizontal;
                    direction.y = -throwJoystick.Vertical;

                    if (PlayerPrefs.HasKey("CONTROL_STATUS") && Convert.ToBoolean(PlayerPrefs.GetInt("CONTROL_STATUS")))
                    {
                        direction.x = throwJoystick.Horizontal;
                        direction.y = throwJoystick.Vertical;
                    }

                    throwDirection = direction;
                }

                if (!initCharge)
                {
                    initCharge = true;
                    audioManager.Play("Charging");
                }
                playerMovement.speed = 1.5f;
                animator.SetBool("Charging", true);
                for (int i = 1; i < points.Length; i++)
                {
                    points[i].SetActive(true);
                    points[i].transform.position = TrajectoryPosition(i * 0.02f);
                    points[i].transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                }
                if (shootForce < maxShootForce)
                {
                    shootForce += Time.deltaTime * chargeSpeed;
                }
                if (shootForce >= maxShootForce && !maxCharge)
                {
                    audioManager.Play("MaxCharge");
                    maxCharge = true;
                }

                if (health.isDead)
                {
                    state = AttackState.cooldown;
                    audioManager.StopPlaying("Charging");
                    for (int i = 0; i < points.Length; i++)
                    {
                        points[i].SetActive(false);
                    }
                }

                //TODO: Ability to cancel attack
                if (throwJoystick.Horizontal == 0 && throwJoystick.Vertical == 0)
                {
                    direction = throwDirection;
                    state = AttackState.active;
                    audioManager.StopPlaying("Charging");
                    initCharge = false;
                }
                break;
            case AttackState.active:
                if (maxCharge)
                {
                    shootForce = 40;
                    audioManager.Play("ChargeThrow");
                }
                else
                {
                    audioManager.Play("RegularThrow");
                }

                maxCharge = false;
                playerMovement.speed = 3f;
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
        if (initialInput)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(projectile, firepoint.position, firepoint.rotation) as GameObject;
        newBullet.GetComponent<Projectile>().speed += shootForce;
        state = AttackState.cooldown;
    }

    Vector2 TrajectoryPosition(float t)
    {
        Vector2 currPointPos = (Vector2)trajectoryPoint.transform.position + (1.25f * direction.normalized * shootForce * t);

        return currPointPos;
    }
}
