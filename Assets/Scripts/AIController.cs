using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
public class AIController : MonoBehaviour
{
    //TODO: keep track of current wayoint.
    //
    private TankData tData;
    private TankShooter tShooter;
    private TankMotor tMotor;

    public TankData td;
    public GameObjects[] waypoints;
    public int currentWaypoint = 0;
    public float closeEnoughToWaypoint = 1.0f;
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
        //see if we are already at point
        //if we're not at a waypoint, face it
        if(RotateTowards(waypoints[currentWaypoint].transform.position, tData.rotateSpeed))
		{
            //do nothing
		}
        // if we are facing the waypoint move towards it
		else
		{
            //move forward
            tMotor.Move(tData.moveSpeed);
		}

        //if we are close enough to waypoints
        if(Vector3.Distance (transform.position, waypoints[currentWaypoint].position) < closeEnoughToWaypoint)
		{
            currentWaypoint++;
		}


        //turn towards waypoint
        td.mover.MoveTo(waypoints[currentWaypointIndex].transform);
        //if we are close enough
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < closeEnoughForWaypoints)
        {
            currentWaypointIndex++;
        }
        //move forward
        //if we are "close enough" to the waypoint, advance to next waypoint
        DoPatrol();
    }

    public void DoPatrol ()
	{
        
        
	}
    
}
