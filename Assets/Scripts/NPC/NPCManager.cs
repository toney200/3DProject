using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    private Rigidbody rb;
    private Animator animator;
    public LayerMask isGround;
    public LayerMask isPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Waypoints
    public List<Transform> waypoints;
    private int currentWaypointIndex = 0;

    //Time to wait
    public float time;
    bool isWaiting;

    //states
    bool isPatroling;
   public bool isGathering;



    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    { 
        Patrol();  
    }

    void Patrol()
    {
        if (!walkPointSet) 
        {
            WayPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        animator.SetBool("isWalking", true);

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void WayPoint()
    {
        
        //Go to set waypoints
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position ) < 1f)
        {
            
            //Loop back to start
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            //StartCoroutine(NPCWait(time));
        }
        
        agent.SetDestination(waypoints[currentWaypointIndex].position);


        //Randomzied patrolling
       /* float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);*/

        /*if(Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }*/
    }


    IEnumerator NPCWait(float time)
    {
        if (!isWaiting)
        {
            isWaiting = true;
          
            yield return new WaitForSeconds(time);

            Debug.Log("Waiting for " + time);
            isWaiting = false;
        }


       
    }


}
