using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovableCharacter
{

    void Awake()
	{
		baseColor = Color.yellow;
		tag = "Enemy";
		title = "EnemyAI";
    
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
		Debug.Log("*** Enemy: I'm going to try this direction: " + otherDirection + " ***");
		ChangeDirectionIfOpen(otherDirection);
	}
}
