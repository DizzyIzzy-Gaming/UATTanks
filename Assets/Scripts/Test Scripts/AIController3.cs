using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class AIController3 : MonoBehaviour
{

    public TankData tData;
    public TankMotor tMotor;
    public TankShooter tShooter;

    public enum AttackState { Chase };
    public AttackState attackState = AttackState.Chase;

    public float avoidanceTime = 2.0f;
    private float exitTime;
     
    public enum AvoidanceStage { NotAvoiding, ObstacleDetected, AvoidingObstacle };
    public AvoidanceStage avoidanceStage = AvoidanceStage.NotAvoiding;
    public float closeEnough = 4f;

	// Start is called before the first frame update
	void Start()
    {
        tData = GetComponent<TankData>();
        tMotor = GetComponent<TankMotor>();
        tShooter = GetComponent<TankShooter>();
    }

	private void Update()
	{
		if (attackState == AttackState.Chase)
		{
            if (avoidanceStage != AvoidanceStage.NotAvoiding)
			{
                Avoid();
            }
            else
			{
                Chase(GameManager.Instance.Players[0]);
            }
		}
	}

    public void Chase(GameObject target)
    {
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
    public void Avoid()
	{

        if (avoidanceStage == AvoidanceStage.ObstacleDetected)
        {

            tMotor.Rotate(-tData.rotateSpeed * Time.deltaTime);
            if(CanMove(tData.moveSpeed))
			{
                avoidanceStage = AvoidanceStage.AvoidingObstacle;

                exitTime = avoidanceTime;
			}
        }
        else if (avoidanceStage == AvoidanceStage.AvoidingObstacle)
		{
            //if we can move forward, do so
            if(CanMove(tData.moveSpeed))
			{
                exitTime -= Time.deltaTime;
                tMotor.Move(tData.moveSpeed);

                if(exitTime <= 0)
				{
                    avoidanceStage = AvoidanceStage.NotAvoiding;		
                }
			}
            else
			{
                avoidanceStage = AvoidanceStage.ObstacleDetected;
			}
		}
        
	}

    public bool CanMove(float speed)
	{
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, speed))//out: whatever raycast hit we wanna have as an output and assign the values of the raycast into the variable
		{
            if(!hit.collider.CompareTag("Player"))
			{
                //cant move
                return false;
			}
		}
        //otherwise return true
        return true;
	}
}
