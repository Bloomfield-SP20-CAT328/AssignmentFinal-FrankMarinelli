using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : MovableCharacter
{
    //For this I looked around at code. Turns out its actually fairly simple, just change around some of the stuff from the first enemy script.
    void Awake()
	{
		baseColor = Color.red;
		tag = "Enemy";
		title = "EnemyAI ButDumb";
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		MoveCharacter();
        if(direction == Direction.None)
		{
			ChooseNewDirection();
		}
    }

    void ChooseNewDirection()
	{
		Direction otherDirection = (Direction)Random.Range(1, System.Enum.GetValues(typeof(Direction)).Length);
		Debug.Log("*** Enemy: I'm going to try this direction till I smack a wall" + otherDirection + "***");
		ChangeDirectionIfOpen(otherDirection, false);
	}
}
