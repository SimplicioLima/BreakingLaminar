using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask obstacleMask, smallObstacleMask;
    public Vector2 gridWorldSize;
    public TerrainType[] walkableRegions;
    LayerMask walkableMask;
    Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

    public float nodeRadius;



    bool walkable;
    Vector3 worldPosition;


    int gridSizeY;
    int gridSizeX;

    int penaltyMin = int.MaxValue;
    int penaltyMax = int.MinValue;

    Node[,] grid;





    private void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        foreach (TerrainType region in walkableRegions)
        {
            walkableMask.value |= region.terrainMask.value;
            walkableRegionsDictionary.Add(Mathf.RoundToInt(Mathf.Log(region.terrainMask.value, 2)), region.terrainPenalty);
        }



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

                int movementPenalty = 0;


                Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100, walkableMask))
                {
                    walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
                }





                grid[x, y] = new Node(worldPoint, walkable, x, y, movementPenalty);

            }
        }

        BlurredPenaltyMap(3);

    }

    void BlurredPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize - 1) / 2;

        int[,] penaltyHorizontalPass = new int[gridSizeX, gridSizeY];
        int[,] penaltyVerticalPass = new int[gridSizeX, gridSizeY];
        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = -kernelExtents; x <= kernelExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltyHorizontalPass[0, y] += grid[sampleX, y].movementPenalty;

            }

            for (int x = 1; x < gridSizeX; x++)
            {
                int removeIndex = Mathf.Clamp(x - kernelExtents - 1, 0, gridSizeX);
                int addIndex = Mathf.Clamp(x + kernelExtents, 0, gridSizeX - 1);
                penaltyHorizontalPass[x, y] = penaltyHorizontalPass[x - 1, y] - grid[removeIndex, y].movementPenalty +
                    grid[addIndex, y].movementPenalty;
            }
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = -kernelExtents; y <= kernelExtents; y++)
            {
                int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                penaltyVerticalPass[x, 0] += penaltyHorizontalPass[x, sampleY];

            }

            for (int y = 1; y < gridSizeY; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernelExtents - 1, 0, gridSizeY);
                int addIndex = Mathf.Clamp(y + kernelExtents, 0, gridSizeY - 1);
                penaltyVerticalPass[x, y] = penaltyVerticalPass[x, y - 1] -
                    penaltyHorizontalPass[x, removeIndex] + penaltyHorizontalPass[x, addIndex];

                int blurredPenalty = Mathf.RoundToInt((float)penaltyVerticalPass[x, y] / (kernelSize * kernelSize));
                grid[x, y].movementPenalty = blurredPenalty;

                if (blurredPenalty > penaltyMax)
                {
                    penaltyMax = blurredPenalty;
                }
                if (blurredPenalty < penaltyMin)
                {
                    penaltyMin = blurredPenalty;
                }

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
    //   private void OnDrawGizmos()
    //   {
    //      Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
    //    if (grid != null)
    // {
    //      foreach (Node node in grid)
    //   {
    //     Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, node.movementPenalty));
    //   Gizmos.color = (node.walkable) ? Gizmos.color : Color.red;
    // Gizmos.DrawCube(node.nodePosition, Vector3.one * (nodeDiameter));
    //  }
    //  }
    // }

    [System.Serializable]
    public class TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPenalty;
    }


}
