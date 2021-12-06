using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] pathGrid;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;
    
    //This is used to establish the player's position in the grid . . . which makes sense . . . cool
    public Transform player;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    private void CreateGrid()
    {
        pathGrid = new Node[gridSizeX, gridSizeY];

        //This is used to establish here in space each node is created and assigning that respective value to the nodes in the 2D array
        Vector2 worldBottomLeft = transform.position - Vector3.right * gridSizeX / 2 - transform.position - Vector3.up * gridSizeY / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //Establish where in the grid this next node will be
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);

                //Oh, I need to set up walkable
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask);

                //Cool, and then with that world point and all this info you can use the constructor from the node class to build a node at that location
                pathGrid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
