using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCharacter : MonoBehaviour
{

	//This should work out of the gate.
    //If not... then I suck.
    //Some of the code here was sampled from Carlo's project. I was looking for Assignment 16 in the class repo and couldnt find it
    //With the way Carlo had his, I just thought you wrote it somewhere and I couldnt find it.
    //Thankfully, all of the code made perfect sense to me. Its basically just taking code done before and adjusting it to make new AI.

	public float moveSpeed = 3.0f;
	protected float visualDistance = 4.0f;
	protected float fieldOfVision = 60.0f;
	protected Color baseColor = Color.blue;
	protected string title = "Movable Player";
	protected string tag = "Untagged";
	protected Point targetLocation = new Point(0, 0);
	protected Direction direction = Direction.None;

    // Start is called before the first frame update
    public void Start()
    {
		gameObject.name = title;
		gameObject.tag = tag;
		gameObject.GetComponent<Renderer>().material.color = baseColor;
		targetLocation = new Point((int)transform.position.x, (int)transform.position.z);
	}

    private void OnDrawGizmos() { DrawRays(new Color(0.0f, 1.0f, 0.0f, 1.0f));  }

    private void DrawRays(Color color)
	{
		Vector3 forward = transform.forward;
		Vector3 frontRayPoint = transform.position + (forward * visualDistance);
		Quaternion lineRotation = Quaternion.Euler(0, fieldOfVision / 2, 0);
		Vector3 leftRayPoint = transform.position + visualDistance * (lineRotation * transform.forward);
		lineRotation = Quaternion.Euler(0, -fieldOfVision / 2, 0);
		Vector3 rightRayPoint = transform.position + visualDistance * (lineRotation * transform.forward);
		Debug.DrawLine(transform.position, frontRayPoint, color);
		Debug.DrawLine(transform.position, leftRayPoint, color);
		Debug.DrawLine(transform.position, rightRayPoint, color);
	}

    protected void MoveCharacter()
	{
        if(direction != Direction.None)
		{
			Vector3 target = new Vector3(targetLocation.x, transform.position.y, targetLocation.y);
            if(transform.position != target)
			{
				transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
			} else
			{
				direction = Direction.None;
			}
		}
	}

	protected bool ChangeDirectionIfOpen(Direction whichDirection, bool onlyGoOneCell = true)
	{
		bool result = false;
		Point offset = Movement.Offsets[(int)whichDirection];
		Point currentPosition = new Point((int)gameObject.transform.position.x, (int)gameObject.transform.position.z);
		Point checkPoint = currentPosition + offset;
		if (GameController.Instance.maze.IsOpen(checkPoint))
		{
			result = true;
			targetLocation = new Point(checkPoint.x, checkPoint.y);
			direction = whichDirection;
            if (!onlyGoOneCell)
			{
                while(GameController.Instance.maze.IsOpen(targetLocation + offset)){
					targetLocation += offset;
				}
			}
		}

		return result;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
