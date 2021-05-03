using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PowerUp 
{
	public float healthModifier = 0f;
	public float maxHealthModifier = 0f;
	public float fireRateModifier = 0f;// adjust cool down timer for shooting. Negative means faster
	public float speedModifier = 0f;

	public float duration = 1f;
	public bool isPermanent = false;

	public void OnActivate(TankData targetData, Health targetHealth)// On activation depending on the variables set by the designer this will add powerup stats
	{
		targetData.moveSpeed += speedModifier;
		targetData.rateOfFire += fireRateModifier;
		targetHealth.maxHealth += maxHealthModifier;
		targetHealth.CurrentHealth += healthModifier;
	}

	public void onDeactivate(TankData targetData, Health targetHealth)// On deactivation depending on the variables set by the designer this will subtract powerup stats
	{
		targetData.moveSpeed -= speedModifier;
		targetData.rateOfFire -= fireRateModifier;
		targetHealth.maxHealth -= maxHealthModifier;
		targetHealth.CurrentHealth -= healthModifier;
	}
}
