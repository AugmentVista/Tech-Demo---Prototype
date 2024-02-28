using UnityEngine;

public class TempStateMachine : MonoBehaviour
{

    // TO DO LIST:
    // GUI legend that displays to the player the first time each seperate state method is invoked.
    // Provide access to Enemy class once that exists. 
    // Encapsulate state machine -- lack of encapsulation is automatic fail (maximum 50% grade)
    bool InRange = true;
    int EnemyHP = 100;
    State currentState;

    void Start()
    {
        currentState = State.Patrol;
    }
    void Update()
    {
        UpdateEnemyStateMachine(); 
    }
    public enum State
    {
        Patrol,
        Chase,
        Search,
        Attack,
        Retreat
    }
    void PatrolState()
    {
        // Patrol Code
        // Change Enemy Material Color to Patrol Color
    }
    void ChaseState()
    {
        if (currentState == State.Patrol)
        {
            currentState = State.Chase;
        }
        // Chase Code
        // Change Enemy Material Color to Chase Color
    }
    void SearchState()
    {
        if (currentState == State.Chase)
        {
            currentState = State.Search;
        }
        // Search Code
        // Change Enemy Material Color to Search Color
    }
    void AttackState() 
    {
        if (currentState == State.Search && InRange)
        {
            currentState = State.Chase;
        }
        // Attack Code
        // Change Enemy Material Color to Attack Color
    }
    void RetreatState()
    {
        if (currentState == State.Attack && EnemyHP > 20)
        {
            currentState = State.Retreat;
        }
        // Retreat Code
        // Change Enemy Material Color to Retreat Color
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
}
// private void UseRockAbility() // Pick this apart to remember raycasting to fixed distance.
//{
//    Vector3 enemySight = enemyTransform.forward;
//    Vector3 enemyPosition = enemyTransform.position + enemySight * enemyRangeOfSight;
//    RaycastHit hit;
//    if (Physics.Raycast(enemyTransform.position, enemySight, out hit, enemyRangeOfSight))
//    {
//        enemyPosition = hit.point;
//    }
//    Call to EnemyStateMachine UpdateStateMachine
//}