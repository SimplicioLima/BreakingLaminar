using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    const float pathUpdateMoveThreshold = .5f;
    const float minPathUpdateTime = .2f;

    public Transform target;
    public float speed = 5;
    public float turnDistance = 3;
    public float turnSpeed = 3;
    public float stoppingDistance = 4;

    PlayerInRange range;
    bool followingPath = true;



    Path path;

    private void Start()
    {
        range = GetComponent<PlayerInRange>();
        StartCoroutine(UpdatePath());
    }




    public void OnPathFound(Vector3[] waypoints, bool success)
    {
        if (success)
        {
            path = new Path(waypoints, transform.position, turnDistance, stoppingDistance);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }
    IEnumerator UpdatePath()
    {
        if (!range.canSeePlayer)
        {
            if (Time.timeSinceLevelLoad < .3f)
            {
                yield return new WaitForSeconds(.3f);
            }
            PathManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPositionOld = target.position;

            while (true)
            {
                yield return new WaitForSeconds(minPathUpdateTime);
                if ((target.position - targetPositionOld).sqrMagnitude > sqrMoveThreshold)
                {
                    PathManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                    targetPositionOld = target.position;
                }
            }
        }
    }

    IEnumerator FollowPath()
    {
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        float speedPercent = 1;

        if (!range.canSeePlayer)
        {
            while (followingPath)
            {
                Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
                while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
                {
                    if (pathIndex == path.finishLineIndex)
                    {
                        followingPath = false;
                        break;
                    }
                    else
                        pathIndex++;
                }
                if (followingPath)
                {
                    if (pathIndex >= path.slowDownIndex && stoppingDistance > 0)
                    {
                        speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDistance);
                        if (speedPercent < 0.01f)
                            followingPath = false;
                    }

                    Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
                    float yPosition = transform.position.y;
                    yPosition = 1;
                    transform.Translate(speed * speedPercent * Vector3.forward * Time.deltaTime);
                }

                yield return null;
            }
        }
    }


    public void OnDrawGizmos()
    {
        if (path != null)
        {
            path.DrawWithGizmos();
        }
    }

}
