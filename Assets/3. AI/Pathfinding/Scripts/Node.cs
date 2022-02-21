using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class Node : IHeapItem<Node>
{
	public bool walkable;
	public Vector3 worldPosition;
	public Node parent;
	public int gridX;
	public int gridY;

	public int gCost;
	public int hCost;
	public int fCost { get { return gCost + hCost; } }

	int heapIndex;
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
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

    public int CompareTo(Node nodeToCompare)
    {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		if (compare == 0)
			compare = hCost.CompareTo(nodeToCompare.hCost);

		return -compare; //negative compare - because the node's value is higher if the fCost or hCost is lower, thus lower cost == higher value
    }
}

