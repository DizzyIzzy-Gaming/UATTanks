using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMover))]

public class TankData : MonoBehaviour
{

    public float moveSpeed;
    public float rotateSpeed;
    public TankMover mover;

	void Start()
	{
		mover = GetComponent<TankMover>();
	}
}
