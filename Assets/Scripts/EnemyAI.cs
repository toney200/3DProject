using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask isGround;
    public LayerMask isPlayer;
    public GameObject projectile;
    public GameObject projectileSpawnPoint;
    Animator animator;
    

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking 
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States 
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Check if player is in sight an attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();

        }

        if (playerInSightRange && !playerInAttackRange) 
        {
            ChasePlayer();
            Debug.Log("Player detected, chasing..");
        }

        if(playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }



    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            WalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void WalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        //Check if gone off map using ground
        if(Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Debug.Log("Player found");
    }

    private void AttackPlayer()
    {
        //Enemy stops moving
        agent.SetDestination(transform.position);
        //Enemy looks at player
        transform.LookAt(player.position);

        if (!alreadyAttacked) { 

            Vector3 spawnPoint = transform.Find("SpawnPoint").position;
            
            Rigidbody rb = Instantiate(projectile, spawnPoint, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
           // rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
