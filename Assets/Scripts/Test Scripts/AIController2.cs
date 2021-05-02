using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
[RequireComponent(typeof(Health))]
public class AIController2 : MonoBehaviour
{

    public TankData tData;
    public TankMotor tMotor;
    public TankShooter tShooter;
    public float fleeDistance = 1f;
    private Health health;
    public float closeEnough = 4f;

    public enum AttackState{ Chase, Flee};
    public AttackState attackState = AttackState.Chase;


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
        if(attackState == AttackState.Chase)
		{
            Chase(GameManager.Instance.Players[0]);
            //Check for transitions
            if (health.currentHealth < 3)
			{
                attackState = AttackState.Flee;
			}
		}
        else if (attackState == AttackState.Flee)
		{
            Flee(GameManager.Instance.Players[0]);
            //Check for transitions
            if (health.currentHealth >= 3)
            {
                attackState = AttackState.Chase;
            }
        }
		else
		{
            Debug.LogWarning("[AIController2] unhandled state in update method");
		}
    }

    public void Chase(GameObject target)
	{
        if(tMotor.RotateTowards(target.transform.position, tData.rotateSpeed))
		{
            //Do Nothing
		}
        else
		{
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= (closeEnough * closeEnough))
			{
                tMotor.Move(tData.moveSpeed);

            }

		}
	}

    public void Flee(GameObject target)
	{
        //Grabs vector to our target
        Vector3 vectorToTarget = target.transform.position - transform.position;

        //get vector away from vector
        Vector3 vectorAwayFromTarget = -1 * vectorToTarget;

        //Normalize the vector away from target
        vectorAwayFromTarget.Normalize();

        // Adjust for flee distance
        vectorAwayFromTarget *= fleeDistance;

        //set flee position
        Vector3 fleePosition = vectorAwayFromTarget + transform.position;
        tMotor.RotateTowards(fleePosition, tData.rotateSpeed);
        tMotor.Move(tData.moveSpeed);
        
	}
}
