using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth = 5;
    public float maxHealth = 5;

    public void TakeDamage(Attack attackData)// handles health, how much damage to take, and if the player should die
	{
        currentHealth -= attackData.attackDamage;

        if(currentHealth <= 0)
		{
            Die();
		}
	}

	private void Die()
	{
		Debug.Log("I Died!!");
		Destroy(this.gameObject);// Temporary way for bot to despawn
		
	}
}
