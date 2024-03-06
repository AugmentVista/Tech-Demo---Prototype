using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Navigation : MonoBehaviour
{
    [SerializeField]
    private Transform Self;
    [SerializeField]
    private Vector3 targetPos1;
    [SerializeField]
    private Vector3 targetPos2;
    [SerializeField]
    private Vector3 targetPos3;
    [SerializeField]
    private Vector3 targetPos4;

    public Transform player;
    private NavMeshAgent agent;
    public AudioClip explosionSound;

    public LayerMask detectionLayer;
    public Vector3 lastPlayerPos;
    public Vector3 previousSelfPos;

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
        agent = GetComponent<NavMeshAgent>();
        previousSelfPos = Self.position;
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
        agent.SetDestination(targetPos1);
        if (Vector3.Distance(Self.position, targetPos1) < 0.1f)
        {
            agent.SetDestination(targetPos2);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
        else if (Vector3.Distance(Self.position, targetPos2) < 0.1f)
        {
            agent.SetDestination(targetPos3);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
        else if (Vector3.Distance(Self.position, targetPos3) < 0.1f)
        {
            agent.SetDestination(targetPos4);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
        else if (Vector3.Distance(Self.position, targetPos4) < 0.1f)
        {
            agent.SetDestination(targetPos1);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
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
        // Change Enemy Material Color to Search Color
        if (Vector3.Distance(Self.position, lastPlayerPos) < 0.1f)
        {
            agent.SetDestination(previousSelfPos);
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
        }
       else if (Vector3.Distance(Self.position, previousSelfPos) < 0.1f)
        {
            agent.SetDestination(lastPlayerPos);
            if (!CanSeePlayer() || (Vector3.Distance(Self.position, lastPlayerPos) < 0.1f))
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
        // Change Enemy Material Color to Attack Color
        explosionSound = Resources.Load<AudioClip>("Explosion");
        int numberOfAttacks = 0;

        if (inRangeOfPlayer())
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= interactionDelay && explosionSound != null)            
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
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
        // Change Enemy Material Color to Retreat Color
        if (CanSeePlayer())
        {
            float distanceToTarget1 = Vector3.Distance(player.position, targetPos1);
            float distanceToTarget2 = Vector3.Distance(player.position, targetPos2);
            float distanceToTarget3 = Vector3.Distance(player.position, targetPos3);
            float distanceToTarget4 = Vector3.Distance(player.position, targetPos4);

            float maxDistance = Mathf.Max(distanceToTarget1, distanceToTarget2, distanceToTarget3, distanceToTarget4);

            Vector3 Safety;

            if (maxDistance == distanceToTarget1)
            {
                Safety = targetPos1;
            }
            else if (maxDistance == distanceToTarget2)
            {
                Safety = targetPos2;
            }
            else if (maxDistance == distanceToTarget3)
            {
                Safety = targetPos3;
            }
            else if (maxDistance == distanceToTarget4)
            {
                Safety = targetPos4;
            }
            else
            {
                Safety = previousSelfPos;
            }
            agent.SetDestination(Safety);
        }
        else
        { 
            currentState = State.Patrol;
        }  
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