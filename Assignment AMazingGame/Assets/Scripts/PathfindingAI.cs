using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
	public int x;
	public int y;
	public int f;



	public Node parent;

	public bool closed;


	public Node(int x, int y, int f, Node parent)
	{
		this.x = x;
		this.y = y;
		this.f = f;
		this.parent = parent;
	}

    public bool SameLocation(Node other)
	{
        if(other.x == this.x && other.y == this.y)
		{
			return true;
		}
		return false;
	}
    public Point ToPoint() { return new Point(this.x, this.y);  }
}

public class PathfindingAI
{
	Maze maze;

	List<Node> openNodes = new List<Node>();
	List<Node> closedNodes = new List<Node>();
	List<Node> solved = new List<Node>();

    public PathfindingAI(Maze maze)
	{
		this.maze = maze;
	}

    public List<Node> FindPath(Point start, Point finish)
	{
		Debug.Log("Im going to try and find a path from: " + start.ToString() + " to " + finish.ToString());
		int distance = Point.Distance(start, finish);
        while(openNodes.Count > 0)
		{

		}
		return solved;
	}

    protected Node NodePop()
	{
		Node lowestNode = openNodes[openNodes.Count - 1];
		for(int i = 0; i < openNodes.Count - 1; i++)
		{
			if (openNodes[i].f < lowestNode.f && !openNodes[i].closed || (lowestNode.closed && !openNodes[i].closed))
			{
				lowestNode = openNodes[i];
			}
		}
		return lowestNode;
		
	}

	protected bool NodeAdd(Node currentNode, Point finish, int xChange, int yChange)
	{
		Point changePoint = new Point(currentNode.x + xChange, currentNode.y + yChange);
		int distance = Point.Distance(changePoint, finish);
		Node nodeToAdd = new Node(changePoint.x, changePoint.y, distance, currentNode);
		openNodes.Add(nodeToAdd);
		return true;


	}
}

