using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBFS
{
    public Vector3 worldPosition;
    public int gridX, gridY;

    //This is used for a* later on, but it's not something I need to worry about yet for BFS
    //int gCost, hCost;


    public NodeBFS (int _gridX, int _gridY, Vector3 _worldPosition)
    {
        gridX = _gridX;
        gridY = _gridY;
        worldPosition = _worldPosition;
    }
}
