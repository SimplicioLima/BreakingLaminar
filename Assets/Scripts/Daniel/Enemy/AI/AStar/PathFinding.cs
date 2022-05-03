using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathFinding : MonoBehaviour
{
    Grid grid;

    private void Awake() => grid = GetComponent<Grid>();



    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;


        Node startNode = grid.WorldPointToNodeCoordinates(request.pathStart);
        Node endNode = grid.WorldPointToNodeCoordinates(request.pathEnd);


        Heap<Node> openSet = new Heap<Node>(grid.MaxGridSize);
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);


        while (openSet.Count > 0)
        {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == endNode)
            {
                pathSuccess = true;
                break;
            }
            foreach (Node neighbourNode in grid.CreateNeighbourNodes(currentNode))
            {
                if (!currentNode.walkable || closedSet.Contains(neighbourNode))
                    continue;

                int movementCost = currentNode.gCost + CalculateCostToMove(currentNode, neighbourNode) + neighbourNode.movementPenalty;

                if (movementCost < neighbourNode.gCost || !openSet.Contains(neighbourNode))
                {
                    neighbourNode.gCost = movementCost;
                    neighbourNode.hCost = CalculateCostToMove(neighbourNode, endNode);
                    neighbourNode.parent = currentNode;

                    if (!openSet.Contains(neighbourNode))
                        openSet.Add(neighbourNode);
                    else
                        openSet.UpdateItem(neighbourNode);
                }
            }

        }
        if (pathSuccess)
            waypoints = ReversePath(startNode, endNode);

        callback(new PathResult(waypoints, pathSuccess,request.callback));
    }



    Vector3[] ReversePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();
        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplePath(path);
        Array.Reverse(waypoints);
        return waypoints;


    }


    Vector3[] SimplePath(List<Node> path)
    {
        List<Vector3> walkablePoints = new List<Vector3>();
        Vector2 olddirection = Vector2.zero;
        for (int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX,
                path[i - 1].gridY - path[i].gridY);

            if (directionNew != olddirection)
                walkablePoints.Add(path[i].nodePosition);

            olddirection = directionNew;
        }
        return walkablePoints.ToArray();
    }



    //calculates the hcost for each node 
    int CalculateCostToMove(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);


        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);

        //diagonal moves cost 14 "points"; sqrt(2) 
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}
