using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
[RequireComponent(typeof(Health))]
public class AIPersonality1 : MonoBehaviour
{

    public TankData tData;
    public TankMotor tMotor;
    public TankShooter tShooter;
    public Health health;
    public float stateExitTime;

    public enum AIPersonality { Guard, Cowardly };
    public AIPersonality personality = AIPersonality.Guard;

    public enum AIStates { Chase, ChaseAndFire, CheckForFlee,Flee }
    public AIStates aiState = AIStates.Chase;
    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
        tMotor = GetComponent<TankMotor>();
        tShooter = GetComponent<TankShooter>();
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (personality)
		{
            case AIPersonality.Guard:
                GuardFSM();
                break;
            case AIPersonality.Cowardly:
                CowardlyFSM();
                break;
            default:
                Debug.LogWarning("[AIPersonality1] Unimplemented finite state machine");
                break;
		}
    }

    public void GuardFSM()
    {
        switch (aiState)
		{
            case AIStates.Chase:
                //Do Behaviours
                Chase();

                //Check for transitions
                if (health.currentHealth < health.maxHealth * 0.5)
                {
                    ChangeState(AIStates.CheckForFlee);
                }
                else if (PlayerIsInRange())
				{
                    ChangeState(AIStates.ChaseAndFire);
				}
                break;


            case AIStates.ChaseAndFire:
                //Do behaviors
                ChaseAndFire();

                //check for transitions
                if(health.currentHealth < health.maxHealth)
				{
                    ChangeState(AIStates.CheckForFlee);
				}
                else if(!PlayerIsInRange())
				{

				}
                break;


            case AIStates.CheckForFlee:
                break;


            case AIStates.Flee:
                break;


            default:
                Debug.LogWarning("[AIPersonality] state doesn't exist");
                break;
		}
    }

	private void ChaseAndFire()
	{
		//TODO: write this method
	}

	private bool PlayerIsInRange()
	{
        return true;
	}

	private void Chase()
	{
		//TODO: Write this method
	}

	public void CowardlyFSM()
	{
        //TODO: Finish behavior
		throw new NotImplementedException();
	}
    void ChangeState(AIStates newState)
	{
        aiState = newState;
        stateExitTime = Time.time;
	}
}
