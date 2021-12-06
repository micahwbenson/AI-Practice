using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
    //you can remove monobehavour here, i guess -- but I don't actually know why that would matter -- I don't really know what's inside of the monobehaviour class tbh
{
    //Ok, for a node you need . . .

    bool walkable;
    public Vector2 worldPosition;

    //g and h cost for the algorithm implementation
    int gCost;
    int hCost;

    //Indices in the 2D array of nodes
    int gridX;
    int gridY;

    //For tracing nodes back through the path
    Node parent;

    //A constructor to build the node whenever it is called
    public Node (bool _walkable, Vector2 worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    //getter to calculate the fcost for each node when needed
    public int fCost
    {
        get
        {
            return hCost + gCost;
        }
    }
}
