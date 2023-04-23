using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotBehaviour : MonoBehaviour
{
    static public System.Random rng = new System.Random();

    Camera cam;
    public NavMeshAgent agent;
    public float patrolMin = 2f, patrolMax = 10f;
    public float patrolForTime = 2f;
    public float agentSpeed = 7.5f;
    public float attackRange;
    private float damageCooldown = 0.1f;
    private float searchCooldown = 0.1f;
    public float scoutingDistance;
    public float alarmDistance = 10f;
    public bool fightingState = false;
    public GameObject Projectile;
    public GameObject[] ProjectileSpawns;
    public RobotCluster Cluster;
    public RobotType Type;
    public RobotState State;
    public Vector3 RTSPatrolGoal;
    private Vector3 lastRTSPatrol;
    private float patrolTimer;
    private GameObject closestEnemy;
    private GameObject player;
    private float enemyDistance = float.MaxValue;
    public RobotRepository rr;
    private RobotStats stats;
    private Animator animator;
    CharacterAudio botSfx;
    void Start()
    {
        if (Type == RobotType.Ranged)
        {
            attackRange = 5f;
            GetComponent<RobotStats>().damage = 5;
        }
        else if (Type == RobotType.Melee)
        {
            attackRange = 1f;
            GetComponent<RobotStats>().damage = 7;
        }

        State = RobotState.Patrol;

        cam = Camera.main;
        rr = cam.GetComponent<RobotRepository>();
        botSfx = GetComponent<CharacterAudio>();
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = 5f;

        if (Cluster == RobotCluster.Adrift)
        {
            agent.enabled = false;
            this.enabled = false;
        }

        stats = GetComponent<RobotStats>();

        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Cluster == RobotCluster.Minion && RTSPatrolGoal != lastRTSPatrol)
        {
            agent.SetDestination(RTSPatrolGoal);
            lastRTSPatrol = RTSPatrolGoal;
        }

        if (agent.remainingDistance > 2f + agent.stoppingDistance)
        {
            agent.speed = agentSpeed * 1.5f;
        }
        else
        {
            if (agent.speed != agentSpeed) agent.velocity = agent.velocity * 0.7f;
            agent.speed = agentSpeed;
        }

        if (State == RobotState.Patrol)
        {
            SearchForEnemy();

            if (fightingState == false)
            {
                agent.stoppingDistance = 0;

                if (agent.remainingDistance <= 0.9f)
                {    
                    patrolTimer += Time.deltaTime;
                    if (patrolTimer > patrolForTime)
                    {
                        SetRandomDestination();
                        patrolTimer = 0f;
                    }
                }
            }
            else if(fightingState == true && agent.remainingDistance >= 0.1f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(agent.destination - transform.position), 1);
                agent.stoppingDistance = attackRange;
                if(agent.remainingDistance-0.1f < attackRange)
                {
                    if (damageCooldown <= 0)
                    {
                        DealDamageToClosestEnemy();
                        damageCooldown += 1f;
                    } else
                    {
                        damageCooldown -= Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            agent.stoppingDistance = 0;
            if (agent.remainingDistance <= 0.9f)
            {
                fightingState = false;
                State = RobotState.Patrol;
            }
        }
    }

    void SetRandomDestination()
    {   
        float randRadius = Random.Range(patrolMin, patrolMax);
        Vector3 randDir = Random.insideUnitSphere * randRadius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, randRadius, -1);
        agent.SetDestination(navHit.position);
    }

    void SearchForEnemy()
    {
        List<GameObject> robotList = new List<GameObject>();
        if (Cluster == RobotCluster.Minion) robotList = rr.RequestEnemyList();
        else robotList = rr.ReqeustMinionList();

        enemyDistance = 100f;
        foreach (GameObject robot in robotList)
        {
            Vector2 distance = new Vector2(Mathf.Abs(transform.position.x - robot.transform.position.x), Mathf.Abs(transform.position.z - robot.transform.position.z));
            if (enemyDistance > distance.magnitude)
            {
                enemyDistance = distance.magnitude;
                closestEnemy = robot;
            }
        }

        Vector2 playerDistance = new Vector2(Mathf.Abs(transform.position.x - player.transform.position.x), Mathf.Abs(transform.position.z - player.transform.position.z));
        if (Cluster != RobotCluster.Minion && enemyDistance > playerDistance.magnitude)
        {
            closestEnemy = player;
            enemyDistance = playerDistance.magnitude;
        }

        if (enemyDistance <= scoutingDistance)
        {
            agent.SetDestination(closestEnemy.transform.position);
            if (fightingState == false)
            {
                fightingState = true;
                if (Cluster == RobotCluster.Enemy) AlarmCompanions();
            }
        }
        else
        {
            fightingState = false;
        }
    }

    void AlarmCompanions()
    {
        foreach (GameObject robot in rr.RequestEnemyList())
        {
            Vector2 distance = new Vector2(Mathf.Abs(transform.position.x - robot.transform.position.x), Mathf.Abs(transform.position.z - robot.transform.position.z));
            if (alarmDistance > distance.magnitude && robot.GetComponent<RobotBehaviour>().fightingState == false)
            {
                robot.GetComponent<NavMeshAgent>().SetDestination(agent.destination);
            }
        }
    }

    void DealDamageToClosestEnemy()
    {
        List<GameObject> robotList = new List<GameObject>();
        if (Cluster == RobotCluster.Minion) robotList = rr.RequestEnemyList();
        else robotList = rr.ReqeustMinionList();

        enemyDistance = float.MaxValue;
        foreach (GameObject robot in robotList)
        {
            Vector2 distance = new Vector2(Mathf.Abs(transform.position.x - robot.transform.position.x), Mathf.Abs(transform.position.z - robot.transform.position.z));
            if (enemyDistance > distance.magnitude)
            {
                enemyDistance = distance.magnitude;
                closestEnemy = robot;
            }
        }

        float playerDistance = new Vector2(Mathf.Abs(transform.position.x - player.transform.position.x), Mathf.Abs(transform.position.z - player.transform.position.z)).magnitude;
        if (Cluster != RobotCluster.Minion && enemyDistance > playerDistance)
        {
            closestEnemy = player;
            enemyDistance = playerDistance;
        }

        if (Type == RobotType.Melee)
        {
            animator.Play("Meele Attack");
            if (closestEnemy.GetComponent<RobotStats>() == null)
                player.GetComponent<PlayerStats>().TakeDamage(GetComponent<RobotStats>().damage);
            else
                closestEnemy.GetComponent<RobotStats>().TakeDamage(GetComponent<RobotStats>().damage);
        }
        else
        {
            if (closestEnemy.GetComponent<RobotStats>() == null)
                SpawnBullet(player);
            else
                SpawnBullet(closestEnemy);
        }
    }

    void SpawnBullet(GameObject target)
    {
        botSfx.AttackSFX();
        Transform startPosition = transform;
        if (ProjectileSpawns.Length == 1)
            startPosition = ProjectileSpawns[0].transform;
        else if (ProjectileSpawns.Length != 0)
        {
            int position = rng.Next(ProjectileSpawns.Length);
            if (ProjectileSpawns[position] != null)
                startPosition = ProjectileSpawns[position].transform;
        }
        GameObject spawned_projectile = Instantiate(Projectile, startPosition.position, Quaternion.identity);
        Bullet bullet = spawned_projectile.GetComponent<Bullet>();
        bullet.Target = target;
        bullet.Damage = stats.damage;
        if (Cluster == RobotCluster.Minion)
            bullet.TargetCluster = RobotCluster.Enemy;
        else
            bullet.TargetCluster = RobotCluster.Minion;
    }
}