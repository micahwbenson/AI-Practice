using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public Vector2 worldPosition;

    public int gridX, gridY;
    public int gCost, hCost;

    public Node parent;

    public Node(bool _walkable, Vector2 worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }
}
