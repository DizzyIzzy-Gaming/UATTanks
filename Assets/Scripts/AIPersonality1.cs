using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Senses))]
public class AIPersonality1 : MonoBehaviour
{

    public TankData tData;
    public TankMotor tMotor;
    public TankShooter tShooter;
    public Health health;
    public Senses mySenses;
    public float stateExitTime;
    public float closeEnough = 4f;
    public float checkDistanceAhead = 5.0f;

    public enum AIPersonality { Guard, Cowardly, Aggressive, Wanderer };
    public AIPersonality personality = AIPersonality.Guard;

    public enum AIStates { Chase, ChaseAndFire, CheckForFlee, Flee, Patrol, RandomPatrol }
    public AIStates aiState = AIStates.Chase;

    public enum AvoidanceStage { NotAvoiding, ObstacleDetected, AvoidingObstacle };
    public AvoidanceStage avoidanceStage = AvoidanceStage.NotAvoiding;
    
    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
        tMotor = GetComponent<TankMotor>();
        tShooter = GetComponent<TankShooter>();
        health = GetComponent<Health>();
        mySenses = GetComponent<Senses>();
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
            case AIPersonality.Aggressive:
                AggressiveFSM();
                break;
            case AIPersonality.Wanderer:
                WandererFSM();
                break;
            default:
                Debug.LogWarning("[AIPersonality1] Unimplemented finite state machine");
                break;
		}
    }

	private void WandererFSM()
	{
		throw new NotImplementedException();
	}

	private void AggressiveFSM()
	{
		throw new NotImplementedException();
	}

	public void GuardFSM()
    {
        switch (aiState)
		{
            case AIStates.Chase:
                //Do Behaviours
                if(canSee)
                Chase();

                //Check for transitions
                if (health.currentHealth < health.maxHealth)
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

	private void Chase(GameObject target)
	{
        //TODO: Write this method
        if (tMotor.RotateTowards(target.transform.position, tData.rotateSpeed))
        {
            //Do Nothing
        }
        else if (!CanMove(tData.moveSpeed))
        {
            avoidanceStage = AvoidanceStage.ObstacleDetected;
        }
        else
        {
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= (closeEnough * closeEnough))
            {
                tMotor.Move(tData.moveSpeed);

            }

        }
    }

	private bool CanMove(float checkDistanceAhead)
	{
		
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
