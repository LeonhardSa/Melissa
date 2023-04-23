using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public float speed;
    public float damage = 10f;
    public float lifeTime = 2f;
    public GameObject explosionEffect;
    void FixedUpdate()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No Speed");
        }
        Destroy(gameObject, lifeTime);
       
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            RobotStats rs = col.collider.transform.GetComponentInParent<RobotStats>();
            rs.TakeDamage(damage);
            rs.RaiseConversion(damage);
        } else if (col.gameObject.tag == "Minion")
        {
            RobotStats rs = col.collider.transform.GetComponentInParent<RobotStats>();
            rs.HealDamage(damage, false);
        } else if (col.gameObject.tag == "Orb")
        {
            col.collider.transform.GetComponent<OrbScript>().TakeDamage(damage);
        }
        speed = 0;
        float radius = 5.0F;
        float power = 10.0F;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
