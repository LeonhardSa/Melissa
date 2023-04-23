using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 400f;
    public float rotationSpeed = 1f;
    public float fireRate = 1f;
    public float attackCost;
    public float bombCost;

    private bool sprint;
    private float exhaustion = 1f;
    private bool runAgain = true;
    private float currentSpeed;
    private float nextTimeToFire = 0f;
    private float moveLockTime = 0f;

    [HideInInspector]
    public Rigidbody rb;
    bool canMove;
    float inputX;
    float inputZ;
    Camera cam;
    Vector3 LookDirection;
    
    PlayerHUD hud;
    PlayerAnimator anim;
    PlayerStats playerStats;


    void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        anim = GetComponentInChildren<PlayerAnimator>();
        hud = GetComponentInChildren<PlayerHUD>();
        rb = GetComponent<Rigidbody>();
        cam=Camera.main;
    }
    void Update()
    {
        if (playerStats.isAlive == false)
        {
            return;
        }

        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            if (playerStats.playerEnergy >= attackCost)
            {
                playerStats.UseEnergy(attackCost);
                Rotation();
                rb.velocity = Vector3.zero;
                nextTimeToFire = Time.time + 1f / fireRate;
                anim.Attack();
                moveLockTime = Time.time + 1f;
            }
            else
            {
                StartCoroutine(hud.DisplayMessage("INSUFFICIENT ENERGY"));
            }
        }
        if (Input.GetButton("Fire2") && Time.time >= nextTimeToFire)
        {
            if (playerStats.playerEnergy >= bombCost)
            {
                playerStats.UseEnergy(bombCost);
                Rotation();
                rb.velocity = Vector3.zero;
                nextTimeToFire = Time.time + 1f / fireRate;
                anim.AltAttack();
                moveLockTime = Time.time + 1.2f;
            }
            else
            {
                StartCoroutine(hud.DisplayMessage("INSUFFICIENT ENERGY"));
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && runAgain)
        {
            currentSpeed = walkSpeed * 1.5f;
            exhaustion -= Time.deltaTime;
            if (exhaustion <= 0f) runAgain = false;
        }
        else
        {
            currentSpeed = walkSpeed;
            if(exhaustion<1f)exhaustion += Time.deltaTime / 2;
            if (exhaustion >= 0.25f) runAgain = true;
        }

    }
    void FixedUpdate()
    {
        
        if (Time.time >= moveLockTime)
        {
            Movement();
        }
    }
    void Movement()
    {
        
        Vector3 movement = new Vector3(inputX, 0, inputZ);
        movement = movement.normalized * Time.deltaTime * currentSpeed;
        rb.AddForce(movement, ForceMode.VelocityChange);
        rb.velocity = new Vector3(0, rb.velocity.y, 0);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), rotationSpeed);
        }

    }

    void Rotation()
    {
        LookDirection = ((Input.mousePosition) - cam.WorldToScreenPoint(transform.position)).normalized;
        LookDirection.z = LookDirection.y;
        LookDirection.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookDirection), 1f);
    }
    


}
