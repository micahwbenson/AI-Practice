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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateGrid()
    {
        grid = new NodeBFS[gridSizeX, gridSizeY];

        //Ok, if I remember this correctly, we need a place to start dropping in Nodes - the bottom left corner - ok, need to refresh a bit on the vector math here . . . but stuff is starting to stick!!!
        //Vector2 gridBottomLeft = transform.position - Vector2.right * gridWorldSize.x / 2 - Vector2.up * nodeDiameter + nodeRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, gridWorldSize);
    }
}
