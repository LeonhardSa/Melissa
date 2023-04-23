using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public float damage = 10f;
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerStats playerHealth = col.collider.transform.GetComponentInParent<PlayerStats>();
            playerHealth.TakeDamage(damage);
        }
    }
}
