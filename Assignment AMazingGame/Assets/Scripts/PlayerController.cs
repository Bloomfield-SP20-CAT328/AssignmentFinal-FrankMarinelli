using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovableCharacter
{
    //I was going to use wasd and getkeydown to move players, until i remembered pollati did not want that.
    //regardless, this hopefully works.
    void Awake()
	{
		baseColor = Color.blue;
		tag = "Player";
		title = "Player";
	}



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		MoveCharacter();
		HandleInput();

	}

    public void HandleInput()
	{
		if (Input.GetAxis("Vertical") > 0)
		{
			ChangeDirectionIfOpen(Direction.Down);
		}else if (Input.GetAxis("Vertical") < 0)
		{
			ChangeDirectionIfOpen(Direction.Up);
		}
		if (Input.GetAxis("Horizontal") > 0)
		{
			ChangeDirectionIfOpen(Direction.Right);
		}else if (Input.GetAxis("Horizontal") < 0)
		{
			ChangeDirectionIfOpen(Direction.Left);
		}
		
	}

    
}
