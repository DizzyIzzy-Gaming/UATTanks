using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 5;
    public float maxHealth = 5;

    public void TakeDamage(Attack attackData)
	{
        currentHealth -= attackData.attackDamage;

        if(currentHealth <= 0)
		{
            Die();
		}
	}

	private void Die()
	{
		throw new NotImplementedException();
	}
}
