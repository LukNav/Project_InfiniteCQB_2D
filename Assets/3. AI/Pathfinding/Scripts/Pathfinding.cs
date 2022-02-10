using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour 
{
    //debugging 
    public Transform seeker, target;
    private void Update()
    {
        FindPath(seeker.position, target.position);
    }



    Grid _grid;
    const int _priceOfDiagonalMove = 14;//due to performance reasons we use ints. We can image this value as 1.4
    const int _priceOfPerpendicularMove = 10;

    void Awake()
    {
        _grid = GetComponent<Grid>();
        if (!_grid)
            Debug.LogError("Grid component not found");
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = _grid.NodeFromWorldPoint(startPos);
        Node targetNode = _grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in _grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        _grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int deltaX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int deltaY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (deltaX > deltaY)
            return (deltaX - deltaY) * _priceOfPerpendicularMove + deltaY * _priceOfDiagonalMove;
        return (deltaY - deltaX) * _priceOfPerpendicularMove + deltaX * _priceOfDiagonalMove;
    }

}
