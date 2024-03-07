using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Navigation : MonoBehaviour
{
    [SerializeField]
    private Transform Self;
    public GameObject targetPointZero;
    public GameObject targetPointOne;
    public GameObject targetPointTwo;
    public GameObject targetpointThree;

    public Vector3 targetPoint0;
    public Vector3 targetPoint1;
    public Vector3 targetPoint2;
    public Vector3 targetPoint3;

    public Transform player;
    private NavMeshAgent agent;
    public AudioClip AttackSound;

    public LayerMask detectionLayer;
    public Vector3 lastPlayerPos;
    public Vector3 previousSelfPos;

    public static int targetswitch = 0;
    public float weakForceThreshold = 10f;
    public float strongForceThreshold = 20f;
    public float sensorRange = 10f;
    public float meleeRange = 3f;
    private float interactionDelay = 3f; 
    private float elapsedTime = 0f; 

    State currentState;
    public enum State
    {
        Patrol,
        Chase,
        Search,
        Attack,
        Retreat
    }
    private void Start()
    {
        targetswitch = 0;
        agent = GetComponent<NavMeshAgent>();
        previousSelfPos = Self.position;
        targetPoint0 = targetPointZero.transform.position;
        targetPoint1 = targetPointOne.transform.position;
        targetPoint2 = targetPointTwo.transform.position;
        targetPoint3 = targetpointThree.transform.position;
    }
    private void Update()
    {
        UpdateEnemyStateMachine();
    }
    void UpdateEnemyStateMachine()
    {
        switch (currentState)
        {
            case State.Patrol:
                PatrolState();
                break;
            case State.Chase:
                ChaseState();
                break;
            case State.Search:
                SearchState();
                break;
            case State.Attack:
                AttackState();
                break;
            case State.Retreat:
                RetreatState();
                break;
        }
    }
    void PatrolState()
    {
        // Change Enemy Material Color to Patrol Color
        Debug.Log("Entering Patrol State");
        Debug.Log("targetswitch is " + targetswitch);
        if (targetswitch == 0)
        {
            agent.SetDestination(targetPoint0);
            Debug.Log("targetswitch is " + targetswitch);
            Debug.Log("Ai pos is " + Self.position);
            if (Vector3.Distance(Self.position, targetPoint0) <= 1.5f)
            {
                targetswitch++;
                Debug.Log("targetswitch is " + targetswitch);
            }
        }
        if (CanSeePlayer())
        {
            currentState = State.Chase;
            targetswitch = 0;
        }
        if (Vector3.Distance(Self.position, targetPoint0) <= 1.5f || targetswitch == 1)
        {
            agent.SetDestination(targetPoint1);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
                targetswitch = 0;
            }
            targetswitch++;
        }
        else if (Vector3.Distance(Self.position, targetPoint1) <= 1.5f || targetswitch == 2)
        {
            agent.SetDestination(targetPoint2);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
                targetswitch = 0;
            }
            targetswitch++;
        }
        else if (Vector3.Distance(Self.position, targetPoint2) <= 1.5f || targetswitch == 3)
        {
            agent.SetDestination(targetPoint3);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
                targetswitch = 0;
            }
            targetswitch++;
        }
        else if (Vector3.Distance(Self.position, targetPoint3) <= 1.5f || targetswitch == 4)
        {
            agent.SetDestination(targetPoint0);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
                targetswitch = 0;
            }
        }
        //else
        //{
        //    targetswitch = 0;
        //}
        currentState = State.Patrol;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 relativeVelocity = collision.relativeVelocity;

            float forceMagnitude = relativeVelocity.magnitude;

            if (forceMagnitude < weakForceThreshold)
            {
                Debug.Log("Weak impact!");
                // Perform actions for weak impact
            }
            else if (forceMagnitude >= weakForceThreshold && forceMagnitude < strongForceThreshold)
            {
                Debug.Log("Medium impact!");
                // Perform actions for medium impact
            }
            else
            {
                Debug.Log("Strong impact!");
                currentState = State.Retreat; 
            }
        }
    }
    void ChaseState()
    {
        Debug.Log("Entering Chase State");
        // Change Enemy Material Color to Chase Color
        if (CanSeePlayer())
        {
            agent.SetDestination(player.position);
            previousSelfPos = Self.position;
            lastPlayerPos = player.transform.position;
        }
        else
        {
            agent.SetDestination(lastPlayerPos);
            currentState = State.Search;
        }
    }
    void SearchState()
    {
        Debug.Log("Entering Search State");
        // Change Enemy Material Color to Search Color
        if (Vector3.Distance(Self.position, lastPlayerPos) < 0.5f)
        {
            agent.SetDestination(previousSelfPos);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
       else if (Vector3.Distance(Self.position, previousSelfPos) < 0.5f)
        {
            agent.SetDestination(lastPlayerPos);
            if (!CanSeePlayer() || (Vector3.Distance(Self.position, lastPlayerPos) < 0.5f))
            {
                currentState = State.Patrol;
            }
            else if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
    }
    void AttackState()
    {
        Debug.Log("Entering Attack State");
        // Change Enemy Material Color to Attack Color
        int numberOfAttacks = 0;

        if (inRangeOfPlayer())
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= interactionDelay && AttackSound != null)            
            {
                AudioSource.PlayClipAtPoint(AttackSound, transform.position);
                elapsedTime = 0;
                numberOfAttacks++;
            }
        }
        if (numberOfAttacks >= 3)
        {
            currentState = State.Retreat;
        }
    }
    void RetreatState()
    {
        currentState = State.Patrol;
        Debug.Log("Entering Retreat State");
        //// Change Enemy Material Color to Retreat Color
        //if (CanSeePlayer())
        //{
        //    float distanceToTarget1 = Vector3.Distance(player.position, targetPoint0);
        //    float distanceToTarget2 = Vector3.Distance(player.position, targetPoint1);
        //    float distanceToTarget3 = Vector3.Distance(player.position, targetPoint2);
        //    float distanceToTarget4 = Vector3.Distance(player.position, targetPoint3);

        //    float maxDistance = Mathf.Max(distanceToTarget1, distanceToTarget2, distanceToTarget3, distanceToTarget4);

        //    Vector3 Safety;

        //    if (maxDistance == distanceToTarget1)
        //    {
        //        Safety = targetPoint0;
        //    }
        //    else if (maxDistance == distanceToTarget2)
        //    {
        //        Safety = targetPoint1;
        //    }
        //    else if (maxDistance == distanceToTarget3)
        //    {
        //        Safety = targetPoint2;
        //    }
        //    else if (maxDistance == distanceToTarget4)
        //    {
        //        Safety = targetPoint3;
        //    }
        //    else
        //    {
        //        Safety = previousSelfPos;
        //    }
        //    agent.SetDestination(Safety);
        //}
        //else
        //{ 
        //    currentState = State.Patrol;
        //}  
    }
    private bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= sensorRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, sensorRange, detectionLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
    private bool inRangeOfPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= sensorRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, meleeRange, detectionLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}