using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TankData))]
public class PowerUpController : MonoBehaviour
{

    private TankData tData;
    public Health tankHealth;
    public List<PowerUp> powerUps = new List<PowerUp>();
    
    

    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
        tankHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        List<PowerUp> expiredPowerUps = new List<PowerUp>();//this is a list that will have powerups that expire added to it
        foreach(PowerUp power in powerUps)
		{
            //subtract from timer
            power.duration -= Time.deltaTime;

            //if time is up, deactivate the power up and remove it from the list
            if(power.duration <= 0)
			{
                expiredPowerUps.Add(power);
			}
		}
        foreach (PowerUp power in expiredPowerUps)// for each of the power ups that have been expired and added to this list we will take away the stats from the object
		{
            power.onDeactivate(tData, tankHealth);
            powerUps.Remove(power);
		}
    }

    public void Add(PowerUp powerUp)//function that adds power up stats to tanks
	{
        powerUp.OnActivate(tData, tankHealth);
        if(!powerUp.isPermanent)
		{
            powerUps.Add(powerUp);
		}
        
	}
}
