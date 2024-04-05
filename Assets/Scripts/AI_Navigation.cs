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
    public GameObject targetpointFour;
    public FirstPersonController Player;
    public int EnemyPower;

    private Vector3 targetPoint;
    private Vector3 targetPoint0;
    private Vector3 targetPoint1;
    private Vector3 targetPoint2;
    private Vector3 targetPoint3;
    private Vector3 targetPoint4;
    private Vector3 Safety;

    public Transform player;
    private NavMeshAgent agent;
    public AudioClip AttackSound;
    public Renderer enemyRenderer;
    public Material baseMaterial;
    public Material PatrolMaterial;
    public Material ChaseMaterial;
    public Material SearchMaterial;
    public Material AttackMaterial;
    public Material RetreatMaterial;
    public LayerMask detectionLayer;
    public Vector3 lastPlayerPos;

    public static int targetswitch = 0;
    public int numberOfAttacks = 0;
    public float weakForceThreshold;
    public float strongForceThreshold;
    public float sensorRange;
    public float meleeRange;
    private float interactionDelay = 2f;
    private float giveUpSearchDelay = 8f;
    private float elapsedTime = 0;
   

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
        enemyRenderer = GetComponent<Renderer>();
        agent = GetComponent<NavMeshAgent>();
        

        targetPoint0 = targetPointZero.transform.position;
        targetPoint1 = targetPointOne.transform.position;
        targetPoint2 = targetPointTwo.transform.position;
        targetPoint3 = targetpointThree.transform.position;
        targetPoint4 = targetpointFour.transform.position;

        baseMaterial = enemyRenderer.material;
        targetPoint = targetPoint0;
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
        enemyRenderer.material = PatrolMaterial;
        // Change Enemy Material Color to Patrol Color

        agent.SetDestination(targetPoint);

        if (CanSeePlayer())
            currentState = State.Chase;

        if (Vector3.Distance(Self.position, targetPoint) <= 5.0f)
        {
            switch (targetswitch)
            {
                case 0:
                    targetPoint = targetPoint1;
                    targetswitch++;
                    break;
                case 1:
                    targetPoint = targetPoint2;
                    targetswitch++;
                    break;
                case 2:
                    targetPoint = targetPoint3;
                    targetswitch++;
                    break;
                case 3:
                    targetPoint = targetPoint4;
                    targetswitch++;
                    break;
                case 4:
                    targetPoint = targetPoint0;
                    targetswitch = 0;
                    break;
            }
        }
    } 
    void ChaseState()
    {
        enemyRenderer.material = ChaseMaterial;
        // Change Enemy Material Color to Chase Color
        if (CanSeePlayer())
        {
            agent.SetDestination(player.position);
            if (inRangeOfPlayer())
            {
                currentState = State.Attack; 
            }
        }
        else
        {
            currentState = State.Search;
        }
    }
    void SearchState()
    {
        enemyRenderer.material = SearchMaterial;
        // Change Enemy Material Color to Search Color
        agent.SetDestination(lastPlayerPos);
        if (!CanSeePlayer())
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= giveUpSearchDelay)
            {
                DetermineSafety();
                currentState = State.Retreat;
                elapsedTime = 0;
                return;
            }
        }
        else if (CanSeePlayer())
        {
            currentState = State.Chase;

        }
    }
    void AttackState()
    {
        enemyRenderer.material = AttackMaterial;
        // Change Enemy Material Color to Attack Color
       
        if (inRangeOfPlayer())
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= interactionDelay && AttackSound != null)
            {
                AudioSource.PlayClipAtPoint(AttackSound, transform.position);
                Player.rb.AddForce(Vector3.forward * EnemyPower, ForceMode.Impulse);
                elapsedTime = 0;
                numberOfAttacks++;
            }
        }
        else 
        { 
            currentState = State.Chase; 
        }
        if (numberOfAttacks >= 3)
        {
            numberOfAttacks = 0;
            currentState = State.Retreat;
            DetermineSafety();
        }
    }
    void RetreatState()
    {
        enemyRenderer.material = RetreatMaterial;
        //// Change Enemy Material Color to Retreat Color

        agent.SetDestination(Safety);
        if (Vector3.Distance(Self.position, Safety) <= 1.5f)
        {
            if (CanSeePlayer())
            {
                currentState = State.Chase;
            }
            else
            {
                currentState = State.Patrol;
            }
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
        if (Vector3.Distance(transform.position, player.position) <= meleeRange)
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
    void DetermineSafety()
    {
        float distanceToTarget0 = Vector3.Distance(player.position, targetPoint0);
        float distanceToTarget1 = Vector3.Distance(player.position, targetPoint1);
        float distanceToTarget2 = Vector3.Distance(player.position, targetPoint2);
        float distanceToTarget3 = Vector3.Distance(player.position, targetPoint3);
        float distanceToTarget4 = Vector3.Distance(player.position, targetPoint4);

        float maxDistance = Mathf.Max(distanceToTarget0, distanceToTarget1, distanceToTarget2, distanceToTarget3, distanceToTarget4);
        if (maxDistance == distanceToTarget0)
        {
            Safety = targetPoint0;
            Debug.Log("Retreat 0");
        }
        else if (maxDistance == distanceToTarget1)
        {
            Safety = targetPoint1;
            Debug.Log("Retreat 1");
        }
        else if (maxDistance == distanceToTarget2)
        {
            Safety = targetPoint2;
            Debug.Log("Retreat 2");
        }
        else if (maxDistance == distanceToTarget3)
        {
            Safety = targetPoint3;
            Debug.Log("Retreat 3");
        }
        else if (maxDistance == distanceToTarget4)
        {
            Safety = targetPoint3;
            Debug.Log("Retreat 4");
        }
        else
        {
            Safety = targetPoint;
            currentState = State.Chase;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float forceMagnitude = collision.relativeVelocity.magnitude;

            //Debug.Log("Force is " + forceMagnitude);
            if (forceMagnitude < weakForceThreshold)
            {
                Debug.Log("Weak impact!");
            }
            else if (forceMagnitude >= weakForceThreshold && forceMagnitude < strongForceThreshold)
            {
                Debug.Log("Medium impact!");
            }
            else
            {
                Debug.Log("Strong impact!");
                currentState = State.Retreat;
                DetermineSafety();
            }
        }
    }
}