using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float TTL;
    public float Damage;
    public float Speed;
    public GameObject Target;
    public RobotCluster TargetCluster;
    public GameObject ParticleTrail;

    private float ttl_counter;
    private Rigidbody rigidbody;
    private ParticleSystem ps;

    private void Start()
    {
        ttl_counter = 0;
        rigidbody = GetComponent<Rigidbody>();

        Vector3 target_p = Target.transform.position;
        target_p.y = transform.position.y;
        transform.LookAt(target_p);

        rigidbody.velocity = transform.forward * Speed;

        ps = ParticleTrail.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        ttl_counter += Time.deltaTime;
        if (ttl_counter > TTL)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        RobotStats target_rs = other.GetComponent<RobotStats>();
        PlayerStats target_ps = other.GetComponent<PlayerStats>();

        if (target_rs == null && other.GetComponent<PlayerStats>() == null)
        {
            Destroy();
            return;
        }

        if (target_rs == null)
        {
            if (TargetCluster != RobotCluster.Minion)
                return;
            target_ps.TakeDamage(Damage);
        }
        else
        {
            if (TargetCluster != other.GetComponent<RobotBehaviour>().Cluster)
                return;
            target_rs.TakeDamage(Damage);
        }

        
        Destroy();
    }

    void Destroy()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponentInChildren<MeshRenderer>().enabled = false;
        GetComponentInChildren<ParticleSystem>().enableEmission = false;
        float spareTime = TTL - ttl_counter;
        if (spareTime < 1)
            ttl_counter -= spareTime;
    }
}