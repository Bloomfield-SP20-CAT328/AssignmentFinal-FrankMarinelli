using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : MovableCharacter
{
	List<Node> pathToPlayer = new List<Node>();
	PathfindingAI pathFinder;
	GameObject target;
	float nextCheck = 0.0f;

	void Awake()
	{
		baseColor = Color.magenta;
		tag = "Enemy";
		title = "Pathfinding AI Enemy";

	}

	public void Start()
	{
		base.Start();
		pathFinder = new PathfindingAI(GameController.Instance.maze);
		target = GameObject.FindGameObjectWithTag("Player");

	}

	void OnDrawGizmos()
	{
		Gizmos.color = baseColor + Color.gray;
		if (pathToPlayer.Count > 0)
		{
			for (int i = 0; i < pathToPlayer.Count - 1; i++)
			{
				Gizmos.DrawLine(new Vector3(pathToPlayer[i].x, 1.0f, pathToPlayer[i].y), new Vector3(pathToPlayer[i + 1].x, 1.0f, pathToPlayer[i + 1].y));
			}
		}
	}

	void Update()
	{
		if (Time.time > nextCheck)
		{
			pathToPlayer = pathFinder.FindPath(new Point(transform.position.x, transform.position.z), new Point(target.transform.position.x, target.transform.position.z));
			Debug.Log(" *************** ");
			Debug.Log(" *** Starting a Pathfind check from " + transform.position.ToString() + " to " + target.transform.position.ToString() + " @ " + Time.time);
            foreach(Node path in pathToPlayer)
			{
				Debug.Log("HERES OUR PATH: " + path.ToPoint().ToString());
			}
			Debug.Log("******* excessive amount of stars ********");
			nextCheck = Time.time + 2;
		}


	}
}
