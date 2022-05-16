using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{

    [SerializeField] int speed;
    [SerializeField] int turnSpeed;
    [SerializeField] int viewDistance;
    [SerializeField] float viewAngle;
    float stoppingDistance;
    float turningDistance;
    int currentIndex;
    bool isPlayerVisible;
    bool followingPath;
    Path path;
    Vector3[] wayPoints;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform pathHolder;
    [SerializeField] Transform enemy;
    Transform player;
    Coroutine patrolCoroutine = null;

    //State Machine variables
    EnemyBaseState currentState;
    EnemyStateFactory states;





    public Path Path { get { return path; } set { path = value; } }
    public EnemyBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Vector3[] Waypoints { get { return wayPoints; } set { wayPoints = value; } }
    public Coroutine PatrolCoroutine { get { return patrolCoroutine; } set { patrolCoroutine = value; } }
    public int Speed { get { return speed; } }
    public int TurnSpeed { get { return turnSpeed; } }
    public int ViewDistance { get { return viewDistance; } }
    public int CurrentIndex { get { return currentIndex; } set { currentIndex = value; } }
    public float ViewAngle { get { return viewAngle; } }
    public float StoppingDistance { get { return stoppingDistance; } set { stoppingDistance = value; } }
    public float TurningDistance { get { return turningDistance; } }
    public LayerMask ObstacleMask { get { return obstacleMask; } }
    public Transform PathHolder { get { return pathHolder; } }
    public Transform Player { get { return player; } }
    public Transform Enemy { get { return enemy; } set { enemy = value; } }
    public bool IsPlayerVisible { get { return isPlayerVisible; } }
    public bool FollowingPath { get { return followingPath; } set { followingPath = value; } }



    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isPlayerVisible = false;
        followingPath = false;
        stoppingDistance = 10f;
        turningDistance = 3f;
        currentIndex = 0;



        states = new EnemyStateFactory(this);
        wayPoints = GeneratePath();
        currentState = states.Patrol();
        currentState.EnterState();

    }

    // Update is called once per frame
    void Update()
    {
        isPlayerVisible = CanSeePlayer();
        Debug.Log(IsPlayerVisible);
        currentState.UpdateState();
    }


    Vector3[] GeneratePath()
    {
        Vector3[] tempPath;

        tempPath = new Vector3[pathHolder.childCount];

        for (int i = 0; i < tempPath.Length; i++)
        {
            tempPath[i] = pathHolder.GetChild(i).position;
            tempPath[i] = new Vector3(tempPath[i].x, 1, tempPath[i].z);
        }
        return tempPath;
    }


    bool CanSeePlayer()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, viewDistance, playerMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(enemy.forward, directionToTarget) < viewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else
            return false;
    }


    private void OnDrawGizmos()
    {
        Vector3 startposition = pathHolder.GetChild(0).position;
        Vector3 previouspos = startposition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.3f);
            Gizmos.DrawLine(previouspos, waypoint.position);
            previouspos = waypoint.position;
        }
        Gizmos.DrawLine(previouspos, startposition);

    }


}
