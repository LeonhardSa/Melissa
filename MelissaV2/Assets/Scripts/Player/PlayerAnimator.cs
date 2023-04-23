using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator animator;
    public PlayerController movement;
    public ShootingScript shoot;
    void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponentInParent<PlayerController>();
        shoot = GetComponentInParent<ShootingScript>();
    }
    void Update()
    {
        animator.SetFloat("speed", movement.rb.velocity.magnitude);
       
    }
    public void Attack()
    {
        animator.SetTrigger("attack");
    }
    public void AltAttack()
    {
        animator.SetTrigger("altAttack");
    }
    public void Hit()
    {
        animator.SetTrigger("takeHit");
    }

    public void Die()
    {
        animator.SetTrigger("die");

    }
    public void Shoot()
    {
        shoot.Fire();
    }
    public void Bomb()
    {
        shoot.Throw();
    }
}
