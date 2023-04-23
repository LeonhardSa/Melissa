using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleForMenu : MonoBehaviour
{
    public NavMeshAgent agent;
    public float patrolMin = 2f, patrolMax = 10f;
    public float patrolForTime = 6f;
    private float patrolTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
    void Update()
    {
        patrolTimer += Time.deltaTime;
        if (patrolTimer > patrolForTime)
        {
            SetRandomDestination();
            patrolTimer = 0f;
        }
    }
    //void Patrol()
    //{
    //    agent.isStopped = false;
        
    //    patrolTimer += Time.deltaTime;
    //    if (patrolTimer > patrolForTime)
    //    {
    //        SetRandomDestination();
    //        patrolTimer = 0f;
    //    }
     
    //}
    void SetRandomDestination()
    {
        float randRadius = Random.Range(patrolMin, patrolMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        agent.SetDestination(navHit.position);
    }
}
