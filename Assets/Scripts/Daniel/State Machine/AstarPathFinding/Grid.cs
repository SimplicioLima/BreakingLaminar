using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask obstacleMask, smallObstacleMask;
    public Vector2 gridWorldSize;


    public float nodeRadius;



    bool walkable;
    Vector3 worldPosition;


    int gridSizeY;
    int gridSizeX;
    int movementPenalty;


    Node[,] grid;


    private void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }


    public float nodeDiameter
    {
        get
        {
            return nodeRadius * 2;
        }
    }

    public int MaxGridSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }


    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2)
            - (Vector3.forward * gridWorldSize.y / 2);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius)
                     + Vector3.forward * (y * nodeDiameter + nodeRadius);



                if (!Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask) &&
                    !Physics.CheckSphere(worldPoint, nodeRadius, smallObstacleMask))
                {
                    walkable = true;
                }
                else
                    walkable = false;


                grid[x, y] = new Node(worldPoint, walkable, x, y, movementPenalty);

            }
        }



    }

    //turns world coordinates from characters to readable values for the grid;
    public Node WorldPointToNodeCoordinates(Vector3 worldPosition)
    {
        //transform player and enemy positon to readable values for the grid;
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        //clamp from 0 to 1 to prevent reaching outside the grid;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);


        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];



    }


    public List<Node> CreateNeighbourNodes(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;


                if (checkX >= 0 && checkX < gridSizeX &&
                    checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }

        }
        return neighbours;
    }
    public List<Node> path;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        foreach (Node node in grid)
        {
            if (path != null)
            {
                if (path.Contains(node))
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(node.nodePosition, Vector3.one * (nodeDiameter - .1f));
                }
            }
        }
    }
}
