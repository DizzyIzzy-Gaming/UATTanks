using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    public TankData td;
    public List<Waypoint> waypoints;
    public int currentWaypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoPatrol();
    }

    public void DoPatrol ()
	{
        //turn towards waypoint
        //move forward
        //if we are "close enough" to the waypoint, advance to next waypoint

	}
}
