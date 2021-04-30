using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMotor))]

public class TankData : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;
    public TankMover mover;
	public float shootingDamage;
	public float rateOfFire;
	public float cannonBallSpeed;

	void Start()
	{
		mover = GetComponent<TankMover>();
	}
}
