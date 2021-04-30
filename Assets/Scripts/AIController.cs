using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class AIController : MonoBehaviour
{

    public TankData tData;
    public TankMotor tMotor;
    public TankShooter tShooter;
    public GameObject[] waypoints;
    public int currentWaypoint = 0;
    public float closeEnough = 1.0f;

    public enum LoopType{ Stop, Loop, PingPong};
    public LoopType loopType = LoopType.Stop;

    private bool isLoopingForward = true;
    // Start is called before the first frame update
    void Start()
    {
        tData = GetComponent<TankData>();
        tMotor = GetComponent<TankMotor>();
        tShooter = GetComponent<TankShooter>();
    }

    // Update is called once per frame
    void Update()
    {



        //turn towards waypoint
        if(tMotor.RotateTowards(waypoints[currentWaypoint].transform.position, tData.rotateSpeed))
		{
            //do nothing
		}
		else
		{
            //move forward
            tMotor.Move(tData.moveSpeed);

		}


        if(loopType == LoopType.Stop)
		{
            if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) < (closeEnough * closeEnough))
            {
                if (currentWaypoint < (waypoints.Length - 1))
                {
                    //Advance to the next waypoint
                    currentWaypoint++;
                }
            }
        }
        else if (loopType == LoopType.Loop)
		{
            
            
            if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) < (closeEnough * closeEnough))
            {
                if(currentWaypoint < (waypoints.Length-1))
				{
                    //Advance to the next waypoint
                    currentWaypoint++;
                }
				else
				{
                    currentWaypoint = 0;
				}
            }
            
			
        }
        else if (loopType == LoopType.PingPong)
		{
            if(isLoopingForward)
			{
                if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) < (closeEnough * closeEnough))
                {
                    if (currentWaypoint < (waypoints.Length - 1))
                    {
                        //Advance to the next waypoint
                        currentWaypoint++;
                    }
                    else
                    {
                        isLoopingForward = false;
                    }
                }
            }
            else
            {
                if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) < (closeEnough * closeEnough))
                {
                    if (currentWaypoint > 0)
                    {
                        //Advance to the next waypoint
                        currentWaypoint--;
                    }
                    else
                    {
                        isLoopingForward = true;
                    }
                }
            }
        }
        else
		{
            Debug.LogWarning("[AIController] Unexpected LoopType");
		}
        
        
        
        //move forward
        //if we are "close enough" to the waypoint, advance to next waypoint


        DoPatrol();
    }

    public void DoPatrol ()
	{
        

	}
}
