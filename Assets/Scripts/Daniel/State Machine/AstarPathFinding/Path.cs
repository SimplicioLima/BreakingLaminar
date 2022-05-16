using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;

    public Path(Vector3[] wayPoints, Vector3 startPos, float turnDistance, float stoppingDistance)
    {
        lookPoints = wayPoints;
        turnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;

        Vector2 previousPoint = FromV3ToV2(startPos);

        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = FromV3ToV2(lookPoints[i]);
            Vector2 directionTocurrentPoint = (currentPoint - previousPoint).normalized;

            Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currentPoint :
                currentPoint - directionTocurrentPoint * turnDistance;

            turnBoundaries[i] = new Line(turnBoundaryPoint,
                previousPoint - directionTocurrentPoint * turnDistance);

            previousPoint = turnBoundaryPoint;
        }

        float distanceFromEndPoint = 0;
        for (int i = lookPoints.Length - 1; i > 0; i--)
        {
            distanceFromEndPoint += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
            if (distanceFromEndPoint > stoppingDistance)
            {
                slowDownIndex = i;
                break;
            }
        }


    }

    Vector2 FromV3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }
}
