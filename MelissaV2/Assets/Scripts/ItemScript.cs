using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public float amount;
    public float radius = 5f;
    public float pickupRadius = 1f;
    public float speed = 1f;
    public float lifetime = 10f;
    public LayerMask layerMask;
    private Vector3 target;
    public bool isHealth, isEnergy;
    private Rigidbody rb;
    
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime);
    }


    void Update()
    {
        rb.transform.Rotate(0, 0, 60f * Time.deltaTime);

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, layerMask);
        if (hits.Length > 0)
        {
            target = hits[0].gameObject.transform.position;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            Collider[] pickup = Physics.OverlapSphere(transform.position, pickupRadius, layerMask);
            if (pickup.Length > 0)
            {
                if (isHealth)
                {
                    PlayerStats stats = pickup[0].GetComponent<PlayerStats>();
                    stats.GiveHealth(amount);
                    
                }
                if (isEnergy)
                {
                    PlayerStats stats = pickup[0].GetComponent<PlayerStats>();
                    stats.GiveEnergy(amount);
                    
                }
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
