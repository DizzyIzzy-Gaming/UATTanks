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
		
        
		

        if(loopType == LoopType.Stop)//goes through the list of waypoints and stops on the last one
        {
            if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
            {
                if (currentWaypoint < (waypoints.Length - 1))
                {
                    //Advance to the next waypoint
                    currentWaypoint++;
                }
            }
			else
			{
                tMotor.Move(tData.moveSpeed);
            }
        }
        else if (loopType == LoopType.Loop)//goes through the list of waypoints and loops back to the first one
        {
            
            
            if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
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
            else
            {
                tMotor.Move(tData.moveSpeed);
            }


        }
        else if (loopType == LoopType.PingPong)//goes through the list of waypoints and goes back in reverse order
        {
            if(isLoopingForward)// going through the waypoints in order
			{
                if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
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
                else
                {
                    tMotor.Move(tData.moveSpeed);
                }
            }
            else // go through them in revers
            {
                if (Vector3.SqrMagnitude(transform.position - waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
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
                else
                {
                    tMotor.Move(tData.moveSpeed);
                }
            }
        }
        else
		{
            Debug.LogWarning("[AIController] Unexpected LoopType");
		}
        



        //move forward
        //if we are "close enough" to the waypoint, advance to next waypoint


        
    }

    
}
