using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;

    public Transform seeker, target;

    // Start is called before the first frame update
    void Awake()
    {
        grid = GetComponent<Grid>();   
    }

    // Update is called once per frame
    void Update()
    {
        FindPath(seeker.position, target.position);
    }

    private void FindPath(Vector2 seekerPos, Vector2 targetPos)
    {
        List<Node> OpenSet = new List<Node>();
        //I don't really know why we are using a hashset for the ClosedSet, but I'll stick with it nonetheless
        HashSet<Node> ClosedSet = new HashSet<Node>();

        Node startNode = grid.NodeFromWorldPoint(seekerPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        while (OpenSet.Count > 0)
        {
            //Ok, we need a variable to store the node with the lowest f-cost
            Node currentNode = OpenSet[0];
            //and then we can loop through each item in the list and calculate the f-cost and store the lowest one
            for (int i = 0; i < OpenSet.Count; i++)
            {
                if (OpenSet[i].fCost < currentNode.fCost || OpenSet[i].fCost == currentNode.fCost && OpenSet[i].hCost < currentNode.hCost)
                {
                    currentNode = OpenSet[i];
                }
            }
            OpenSet.Remove(currentNode);
            ClosedSet.Add(currentNode);
            
            //I think we need to use the Node to world position here -- I forgot to transform my starting and target nodes up above and store them as variable Nodes
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || ClosedSet.Contains(neighbour))
                {
                    continue;
                }

                //Ok, you need a variable to hold the movement cost to the next neighbour, interesting -- aand you need the GetDistance function as part of that calculation
                //So basically, you are calculating a smaller fcost to the neighbour and comparing that against the other neighbours . . . i think
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !OpenSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;

                    if (!OpenSet.Contains(neighbour))
                    {
                        OpenSet.Add(neighbour);
                    }
                }
            }
        }

        //Ok, now I need to set up the GetNeighbours function in grid and then loop through everything . . . hmmm

    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
