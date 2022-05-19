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
    float sensorLength;
    float avoidingDistance;
    Vector3 sensorAdderVector;
    int currentIndex;
    bool isPlayerVisible;
    bool followingPath;
    bool avoidingObstacle;
    Vector3[] wayPoints;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] LayerMask smallObstacleMask;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform pathHolder;
    [SerializeField] Transform enemy;
    Transform player;
    Colision collision;


    //animation stuff
    Animator anim;
    bool isPatroling;




    //State Machine variables
    EnemyBaseState currentState;
    EnemyStateFactory states;





    public EnemyBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Vector3[] Waypoints { get { return wayPoints; } set { wayPoints = value; } }
    public int Speed { get { return speed; } }
    public int TurnSpeed { get { return turnSpeed; } }
    public int ViewDistance { get { return viewDistance; } }
    public int CurrentIndex { get { return currentIndex; } set { currentIndex = value; } }
    public float ViewAngle { get { return viewAngle; } }
    public float StoppingDistance { get { return stoppingDistance; } set { stoppingDistance = value; } }
    public float TurningDistance { get { return turningDistance; } }
    public float SensorLength { get { return sensorLength; } }
    public float AvoidDistance { get { return avoidingDistance; } set { avoidingDistance = value; } }
    public Vector3 SensorAdderVector { get { return sensorAdderVector; } }
    public LayerMask ObstacleMask { get { return obstacleMask; } }
    public LayerMask SmallObstacleMask { get { return smallObstacleMask; } }
    public Transform PathHolder { get { return pathHolder; } }
    public Transform Player { get { return player; } }
    public Transform Enemy { get { return enemy; } set { enemy = value; } }
    public Colision Collision { get { return collision; } set { collision = value; } }
    public bool IsPlayerVisible { get { return isPlayerVisible; } }
    public bool FollowingPath { get { return followingPath; } set { followingPath = value; } }
    public bool AvoidingObstacle { get { return avoidingObstacle; } set { avoidingObstacle = value; } }





    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isPlayerVisible = false;
        followingPath = false;
        stoppingDistance = 5f;
        turningDistance = 5f;
        sensorLength = 4f;
        sensorAdderVector = new Vector3(0, 0.2f, 0.3f);
        avoidingDistance = 1f;
        currentIndex = 0;

        states = new EnemyStateFactory(this);

        wayPoints = GeneratePath();

        currentState = states.Patrol();
        //anim.SetBool("isPatroling", true);
        currentState.EnterState();


    }



    // Update is called once per frame
    void Update()
    {
        isPlayerVisible = CanSeePlayer();
        currentState.UpdateState();
    }



    Vector3[] GeneratePath()
    {
        Vector3[] tempPath;

        tempPath = new Vector3[pathHolder.childCount];

        for (int i = 0; i < tempPath.Length; i++)
        {
            tempPath[i] = pathHolder.GetChild(i).position;
            tempPath[i] = new Vector3(tempPath[i].x, enemy.position.y + 1.5f, tempPath[i].z);
        }
        return tempPath;
    }


    bool CanSeePlayer()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(enemy.position, viewDistance, playerMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionToTarget = (target.position - enemy.position).normalized;

            if (Vector3.Angle(enemy.forward, directionToTarget) < viewAngle / 2f)
            {
                float distanceToTarget = Vector3.Distance(enemy.position, target.position);

                if (!Physics.Raycast(enemy.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    Debug.Log("I can see you!");
                    return true;
                }
                else
                {
                    Debug.Log("Object in the way!");
                    return false;
                }
            }
            else
            {
                Debug.Log("Angles differ!");
                return false;
            }
        }
        else
        {
            Debug.Log("Not in range!");
            return false;
        }
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


    public Vector3 DirectionFromAngle(float angleInDeg, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDeg += enemy.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg) * Mathf.Deg2Rad, 0, Mathf.Cos(angleInDeg) * Mathf.Deg2Rad);
    }



}


