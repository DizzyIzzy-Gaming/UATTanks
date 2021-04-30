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

	void Start()
	{
		mover = GetComponent<TankMover>();
	}
}
