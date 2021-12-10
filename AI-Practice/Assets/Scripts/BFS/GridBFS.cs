using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBFS : MonoBehaviour
{
    NodeBFS[,] grid;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    int gridSizeX;
    int gridSizeY;
    float nodeDiameter;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        grid = new NodeBFS[gridSizeX, gridSizeY];

        //Ok, if I remember this correctly, we need a place to start dropping in Nodes - the bottom left corner - ok, need to refresh a bit on the vector math here . . . but stuff is starting to stick!!!
        Vector3 gridBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        //Ok now you need to loop through your gridSizeX and GridSizeY and instantiate a node at each point . . . even though my node class doesn't currently have a constructor in it
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //Ok so we need a vector for each location so it's a calculation based off the x and y variables and using the gridBottomLeft as a starting point
                Vector2 worldPoint = gridBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                //Ok, now we have a point . . . so using that we want to use the constructor created in the node class to build a node at this location
                grid[x, y] = new NodeBFS(x, y, worldPoint);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, gridWorldSize);

        if (grid != null)
        {
            Gizmos.color = Color.white;
            foreach (NodeBFS n in grid)
            {
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.05f));
            }
        }
    }
}
