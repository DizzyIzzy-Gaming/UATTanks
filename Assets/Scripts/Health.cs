using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	private TankData tData;
    private float currentHealth;
	public float CurrentHealth
	{
		get
		{
			return currentHealth;
		}
		set
		{
			currentHealth = value;
			if(currentHealth <= 0 )
			{
				Die();
				
				
			}
			if (currentHealth > maxHealth)
			{
				currentHealth = maxHealth;
			}
		}
	}
    public float maxHealth = 10f;


	private void Start()
	{
		currentHealth = maxHealth;
		tData = GetComponent<TankData>();
	}
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
		if(!HasAIPersonalities())
		{
			if (tData.lives >= 0)
			{
				GameManager.Instance.SpawnPlayers();
			}
		}
		
		
	}

	private bool HasAIPersonalities()
	{
		return this.gameObject.GetComponent<AIPersonalities>() != null;
	}
}
