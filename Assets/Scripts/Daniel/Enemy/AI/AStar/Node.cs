using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool walkable;
    public Vector3 nodePosition;


    public int gridX;
    public int gridY;


    public int hCost;
    public int gCost;


    public int movementPenalty;

    public Node parent;

    int heapIndex;


    public Node(Vector3 _nodePosition, bool _walkable, int _gridX, int _gridY,int _movementPenalty)
    {
        nodePosition = _nodePosition;
        walkable = _walkable;
        gridX = _gridX;
        gridY = _gridY;
        movementPenalty = _movementPenalty;
    }



    public int FCost
    {
        get
        {
            return hCost + gCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node other)
    {
        int compare = FCost.CompareTo(other.FCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(other.hCost);

        }
        return -compare;
    }
}

