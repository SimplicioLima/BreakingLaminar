using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(1, 20)]
    public int speed;


    public int turnSpeed;


    [Range(1, 10)]
    public int viewDistance;


    const float waitTime = 0.5f;
    float visionAngle;
    Vector3[] wayPoints;

    public LayerMask obstacleMask;
    public Transform pathHolder;
    Transform player;

    Color gizmoColor;
    Color originalColor;


    private void Start()
    {
        gizmoColor = Color.green;
        originalColor = gizmoColor;
        visionAngle = 90f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wayPoints = new Vector3[pathHolder.childCount];


        for (int i = 0; i < wayPoints.Length; i++)
        {
            wayPoints[i] = pathHolder.GetChild(i).position;
            wayPoints[i] = new Vector3(wayPoints[i].x, transform.position.y, wayPoints[i].z);
        }

        StartCoroutine(FollowPath(wayPoints));

    }   


    private void Update()
    {
        if (CanSeePlayer())
        {
            gizmoColor = Color.red;
        }
        else
        {
            gizmoColor = originalColor;
        }
    }




    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer < visionAngle / 2f)
            {
                if (!Physics.Linecast(transform.position,player.position,obstacleMask))
                {
                    return true;
                }
            }

        }
        return false;
    }



    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 directionToTarget = (lookTarget - transform.position).normalized;
        float targetAngle = Mathf.Atan2(directionToTarget.x, directionToTarget.z) * Mathf.Rad2Deg;


        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWayPointIndex = 1;
        Vector3 targetWayPoint = waypoints[targetWayPointIndex];
        transform.LookAt(targetWayPoint);



        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if (transform.position == targetWayPoint)
            {
                targetWayPointIndex = (targetWayPointIndex + 1) % waypoints.Length;
                targetWayPoint = waypoints[targetWayPointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWayPoint));

            }
            yield return null;
        }



    }


    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform wayPoint in pathHolder)
        {
            Gizmos.DrawSphere(wayPoint.position, .3f);
            Gizmos.DrawLine(previousPosition, wayPoint.position);
            previousPosition = wayPoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = gizmoColor;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);


    }




}
