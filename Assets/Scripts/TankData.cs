using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMotor))]

public class TankData : MonoBehaviour
{

    public float moveSpeed;
	public float reverseSpeed;
    public float rotateSpeed;
	public float shootingDamage;
	public float rateOfFire;
	public float cannonBallSpeed;
	public float temporaryScore;//will temporarily be here
	public float cannonBallTimeOut;//how long they are instantiated for without hitting anything
	

	void Start()
	{
		
	}

	//Temporary set up for adding to score
	void AddScore()
	{

		temporaryScore++;
	}
}
