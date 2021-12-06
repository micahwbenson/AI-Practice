using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    Node[,] pathGrid;
    public Vector2 worldGridSize;
    int gridSizeX, gridSizeY;
    public float nodeRadius;
    float nodeDiameter;
    public LayerMask unwalkableMask;

    public Transform playerTf;

    public List<Node> path;

    // Start is called before the first frame update
    void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(worldGridSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(worldGridSize.y / nodeDiameter);

        CreateGrid();
    }

    //This is used both to identify the player node location as well as pathfinding later on in the process . . . this was a pretty good day of review . . .
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + worldGridSize.x) / worldGridSize.x;
        float percentY = (worldPosition.y + worldGridSize.y) / worldGridSize.y;

        //You could also perform this remapping calculation manually . . . this is sort of a LERP function right? . . . anyway
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt(percentX);
        int y = Mathf.RoundToInt(percentY);

        return pathGrid[x, y];
    }

    private void CreateGrid()
    {
        //Need to first establish the 2D array for each node
        pathGrid = new Node[gridSizeX, gridSizeY];

        Vector2 bottomLeftWorldPosition = transform.position - Vector3.right * worldGridSize.x / 2 - Vector3.up * worldGridSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                //Things you need in every location . . . you need a world position and you need a way to establish that relative to the larger grid, so you need the bottom left world position
                //So now you need to establish a worldpoint for each instantiated node
                Vector2 worldPoint = bottomLeftWorldPosition + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);

                //You also need to establish the bool walkable and then you have all the points needed to establish a node
                bool walkable = !Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask);
                pathGrid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node n)
    {
        //We need the list of neighbour nodes to return at the end of this function
        List<Node> neighbours = new List<Node>();
        
        //Ok, you need to do a 3x3 loop around node N's current position
        for (int x = -1; x < 1; x++)
        {
            for (int y = -1; y < 1; y++)
            {
                if (x == 0 && y ==0)
                {
                    continue;
                }

                int checkX = n.gridX + x;
                int checkY = n.gridY + y;

                if (checkX > 0 && checkX <= worldGridSize.x && checkY > 0 && checkX <= worldGridSize.x)
                {
                    neighbours.Add(pathGrid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(worldGridSize.x, worldGridSize.y, 1));

        if (pathGrid != null)
        {
            Node playerNode = NodeFromWorldPoint(playerTf.position);
            foreach (Node n in pathGrid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                //if (path.Contains(n))
                //{
                //    Gizmos.color = Color.black;
                //}
                Gizmos.DrawWireCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.05f));
            }
        }
    }
}
