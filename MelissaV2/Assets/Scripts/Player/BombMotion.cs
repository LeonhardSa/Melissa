using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMotion : MonoBehaviour
{
    public float speed;
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
        if (col.gameObject.tag != "Player")
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
}
