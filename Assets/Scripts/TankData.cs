using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMover))]

public class TankData : MonoBehaviour
{
	
    public float moveSpeed;
    public float rotateSpeed;
    public TankMover mover;
	public float shootDelay;
	public float playerHealth;
	public float playerMaxHealth;
	public float shootingDamage;
	public float shootingRange;

	void Start()
	{
		mover = GetComponent<TankMover>();
	}
}
