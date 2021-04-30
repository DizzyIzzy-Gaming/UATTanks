using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
	public GameObject attacker;
	public float attackDamage = 0.0f;

	public Attack(GameObject Attacker, float Damage)
	{
		attackDamage = Damage;
		attacker = Attacker;
	}
}
