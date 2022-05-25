using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyStateMachine : MonoBehaviour
{

    [SerializeField] int viewDistance;
    [SerializeField] float viewAngle;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] NavMeshSurface surface;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] Transform pathHolder;
    int currentIndex;
    bool isPlayerVisible;
    bool pathRestarted;
    bool hasArrivedAtPlayer;
    bool hasArrivedAtPathHolder;
    float baseStopingDistance;

    Vector3[] wayPoints;
    Transform player;


    //animation stuff
    Animator anim;
    bool isPatroling;


    //State Machine variables
    EnemyBaseState currentState;
    EnemyStateFactory states;





    public EnemyBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Vector3[] Waypoints { get { return wayPoints; } set { wayPoints = value; } }
    public int ViewDistance { get { return viewDistance; } }
    public int CurrentIndex { get { return currentIndex; } set { currentIndex = value; } }
    public float ViewAngle { get { return viewAngle; } }
    public Transform Player { get { return player; } }
    public bool IsPlayerVisible { get { return isPlayerVisible; } }
    public bool PathRestarted { get { return pathRestarted; } set { pathRestarted = value; } }
    public bool HasArrivedAtPlayer { get { return hasArrivedAtPlayer; } set { hasArrivedAtPlayer = value; } }
    public bool HasArrivedAtPathHolder { get { return hasArrivedAtPathHolder; } set { hasArrivedAtPathHolder = value; } }
    public float BaseStoppingDistance { get { return baseStopingDistance; } }


    public NavMeshAgent Agent { get { return agent; } set { agent = value; } }




    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        baseStopingDistance = agent.stoppingDistance;


        states = new EnemyStateFactory(this);

        wayPoints = GeneratePath();
        surface.BuildNavMesh();



        transform.position = wayPoints[0];
        currentState = states.Patrol();
        currentState.EnterState();


    }



    // Update is called once per frame
    void Update()
    {
        isPlayerVisible = PlayerVisible();
        currentState.UpdateState();
    }



    Vector3[] GeneratePath()
    {
        Vector3[] tempPath;

        tempPath = new Vector3[pathHolder.childCount];

        for (int i = 0; i < tempPath.Length; i++)
        {
            tempPath[i] = pathHolder.GetChild(i).position;
            tempPath[i] = new Vector3(tempPath[i].x, transform.position.y /*+ 1.5f*/, tempPath[i].z);
        }
        return tempPath;
    }


    bool PlayerVisible()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < viewAngle / 2f)
                if (!Physics.Linecast(transform.position, player.position, obstacleMask))
                    return true;
        }
        return false;
    }



    public Vector3 DirectionFromAngle(float angleInDeg, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
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


