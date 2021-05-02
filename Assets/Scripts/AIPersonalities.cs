using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankData))]
[RequireComponent(typeof(TankMotor))]
[RequireComponent(typeof(TankShooter))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Senses))]


public class AIPersonalities : MonoBehaviour
{

//all the Components that the AI needs
    private TankData tData;
    private TankMotor tMotor;
    private TankShooter tShooter;
    private Health health;
    private Senses mySenses;
//Flee Variables
    public float fleeDistance = 5.0f; // How far the AI flees 
    public float fleeTime = 30.0f; // How long you want the AI to flee for
    public float timeToCheckForPlayer = 10.0f;
    //Variables for Patroling Waypoints
    private bool isLoopingForward = true;
    public int currentWaypoint = 0;
//Variables for the AI to keep track of the player they had seen or heard
    private GameObject playerSeen = null;
    private GameObject playerHeard = null;

    private float exitTime; // countdown timer to kick an AI out of a state or method
    public float avoidanceTime = 2.0f; // How long you want the AI to be avoiding an obstacle before it checks again
    public float enemyRangeForFire = 4.0f; //How close the AI needs to be before it can Fire at the player
    public float stateExitTime;//gives how long an AI was in a state for
    public float closeEnough = 4.0f; // a distance to check if the AI is close enough to something
    public float checkDistanceAhead = 5.0f; // the distance the AI will check ahead for an obstacle


    // The personalites
    public enum AIPersonality { Guard, Cowardly, Aggressive, Wanderer };
    public AIPersonality personality = AIPersonality.Guard;

    // The States the AI can be in
    public enum AIStates { Chase, ChaseAndFire, CheckForFlee, Flee, Patrol, RandomPatrol, RotateInPosition, CheckForPlayer }
    public AIStates aiState = AIStates.Chase;

    //the variables and enums for obstacle avoidance
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
        switch (personality) //Base on the chose Personality this will go to its respective finite-state machine
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
                Debug.LogWarning("[AIPersonality] Unimplemented finite state machine");
                break;
		}
    }

                                    // This section houses all the Personality finite state machines

    //Will randomly patrol waypoints and look for player
	private void WandererFSM()
	{
        switch (aiState)
        {
            // Patrols set waypoints
            case AIStates.RandomPatrol:
                if (avoidanceStage == AvoidanceStage.NotAvoiding) // checks to see if we are avoiding something
                {
                    RandomPatrol();
                }
                else
                {
                    Avoid();
                }

                //check for transition
                foreach (GameObject player in GameManager.Instance.Players) // checks to see if we can either see or hear the player
                {
                    if (mySenses.CanSee(player))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = player;
                        ChangeState(AIStates.Chase);

                    }
                    else if (mySenses.CanHear(player))
                    {
                        Debug.Log("I heard something!");
                        playerHeard = player;
                        ChangeState(AIStates.CheckForPlayer);
                    }
                }

                break;


            case AIStates.CheckForPlayer: //if the player is heard, turns towards where they heard the player and checks if they can see them

                //Do Behavior
                if(exitTime > 0)
                {
                    tMotor.RotateTowards(playerHeard.transform.position, tData.rotateSpeed);

                    //Check for Transition
                    if (mySenses.CanSee(playerHeard))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = playerHeard;
                        ChangeState(AIStates.Chase);

                    }
                    exitTime -= Time.deltaTime;
                }
                else
                {
                    ChangeState(AIStates.RandomPatrol);
                }
                break;

            //Chases the player that they see
            case AIStates.Chase:
                //Do Behaviours

                if (avoidanceStage == AvoidanceStage.NotAvoiding)//checks to see if we are avoiding something
                {
                    Chase(playerSeen);
                }
                else
                {
                    Avoid();
                }


                //Check for transitions
                if (health.currentHealth < health.maxHealth) //Checks if the AI is low health if it is, flee
                {
                    ChangeState(AIStates.CheckForFlee);
                }
                else if (PlayerIsInRange(playerSeen)) // otherwise if the player is in range and health is good chase and fire at player
                {
                    ChangeState(AIStates.ChaseAndFire);
                }
                break;

            // chases and fires at the player
            case AIStates.ChaseAndFire:

                //Do behaviors
                if (avoidanceStage == AvoidanceStage.NotAvoiding)
                {
                    ChaseAndFire(playerSeen);
                }
                else
                {
                    Avoid();
                }


                //check for transitions
                if (health.currentHealth < health.maxHealth * 0.5)
                {
                    ChangeState(AIStates.CheckForFlee);
                }

                else if (!PlayerIsInRange(playerSeen)) // if player is no longer in range go back to chasing them
                {
                    ChangeState(AIStates.Chase);
                }
                break;

            //Checks to see if the player is still close if true, continue to flee
            case AIStates.CheckForFlee:
                if (PlayerIsInRange(playerSeen))
                {
                    ChangeState(AIStates.Flee);
                    exitTime = fleeTime;
                }
                else if (!PlayerIsInRange(playerSeen))
                {
                    ChangeState(AIStates.RandomPatrol);
                }
                break;

            //Runs away from the player
            case AIStates.Flee:
                //Do Behaviour

                if (avoidanceStage == AvoidanceStage.NotAvoiding)
                {
                    Flee(playerSeen);
                }
                else
                {
                    Avoid();
                }

                //check for transition
                if (exitTime > 0)// this counts down for how long the AI has been fleeing after time is up go back to CheckForFlee
                {
                    exitTime -= Time.deltaTime;
                }
                else
                {
                    ChangeState(AIStates.CheckForFlee);
                }
                break;


            default:
                Debug.LogWarning("[AIPersonality] state doesn't exist");
                break;
        }
    }

	//will patrol waypoints like the Guard but this personality doesn't Flee for any reason at all
	private void AggressiveFSM()
	{
        switch (aiState)
        {
            // Patrols set waypoints
            case AIStates.Patrol:
                if (avoidanceStage == AvoidanceStage.NotAvoiding) // checks to see if we are avoiding something
                {
                    Patrol();
                }
                else
                {
                    Avoid();
                }

                //check for transition
                foreach (GameObject player in GameManager.Instance.Players) // checks to see if we can either see or hear the player
                {
                    if (mySenses.CanSee(player))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = player;
                        ChangeState(AIStates.Chase);

                    }
                    else if (mySenses.CanHear(player))
                    {
                        Debug.Log("I heard something!");
                        playerHeard = player;
                        ChangeState(AIStates.CheckForPlayer);
                    }
                }

                break;


            case AIStates.CheckForPlayer: //if the player is heard, turns towards where they heard the player and checks if they can see them

                //Do Behavior
                if (exitTime > 0)
                {
                    tMotor.RotateTowards(playerHeard.transform.position, tData.rotateSpeed);

                    //Check for Transition
                    if (mySenses.CanSee(playerHeard))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = playerHeard;
                        ChangeState(AIStates.Chase);

                    }
                    exitTime -= Time.deltaTime;
                }
                else
                {
                    ChangeState(AIStates.Patrol);
                }
                break;

            //Chases the player that they see
            case AIStates.Chase:
                //Do Behaviours

                if (avoidanceStage == AvoidanceStage.NotAvoiding)//checks to see if we are avoiding something
                {
                    Chase(playerSeen);
                }
                else
                {
                    Avoid();
                }

                //Check for transitions
                if (PlayerIsInRange(playerSeen)) // otherwise if the player is in range and health is good chase and fire at player
                {
                    ChangeState(AIStates.ChaseAndFire);
                }
                break;

            // chases and fires at the player
            case AIStates.ChaseAndFire:

                //Do behaviors
                if (avoidanceStage == AvoidanceStage.NotAvoiding)
                {
                    ChaseAndFire(playerSeen);
                }
                else
                {
                    Avoid();
                }

                //Check for transition
                if (!PlayerIsInRange(playerSeen)) // if player is no longer in range go back to chasing them
                {
                    ChangeState(AIStates.Chase);
                }
                break;


            default:
                Debug.LogWarning("[AIPersonality] state doesn't exist");
                break;
        }
    }

    // will patrol the waypoints guarding the area
	public void GuardFSM()
    {
        switch (aiState)
		{
            // Patrols set waypoints
            case AIStates.Patrol:
                if(avoidanceStage == AvoidanceStage.NotAvoiding) // checks to see if we are avoiding something
                {
                    Debug.Log("I am patroling");
                    Patrol();
                }
                else
				{
                    Avoid();
				}
                
                //check for transition
                foreach (GameObject player in GameManager.Instance.Players) // checks to see if we can either see or hear the player
                {
                    if (mySenses.CanSee(player))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = player;
                        ChangeState(AIStates.Chase);

                    }
                    else if (mySenses.CanHear(player))
                    {
                        Debug.Log("I heard something!");
                        playerHeard = player;
                        ChangeState(AIStates.CheckForPlayer);
                    }
					else
					{
                        //do nothing
					}
                }

                break;

                
            case AIStates.CheckForPlayer: //if the player is heard, turns towards where they heard the player and checks if they can see them

                //Do Behavior
                if (exitTime > 0)
                {
                    tMotor.RotateTowards(playerHeard.transform.position, tData.rotateSpeed);

                    //Check for Transition
                    if (mySenses.CanSee(playerHeard))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = playerHeard;
                        ChangeState(AIStates.Chase);

                    }
                    exitTime -= Time.deltaTime;
                }
                else
                {
                    ChangeState(AIStates.Patrol);
                }
                break;

                //Chases the player that they see
            case AIStates.Chase:
                //Do Behaviours

                if (avoidanceStage == AvoidanceStage.NotAvoiding)//checks to see if we are avoiding something
                {
                    Chase(playerSeen);
                }
                else
                {
                    Avoid();
                }


                //Check for transitions
                if (health.currentHealth < health.maxHealth) //Checks if the AI is low health if it is, flee
                {
                    ChangeState(AIStates.CheckForFlee);
                }
                else if (PlayerIsInRange(playerSeen)) // otherwise if the player is in range and health is good chase and fire at player
                {
                    ChangeState(AIStates.ChaseAndFire);
                }
                break;

            // chases and fires at the player
            case AIStates.ChaseAndFire:
                
                //Do behaviors
                if (avoidanceStage == AvoidanceStage.NotAvoiding) 
                {
                    ChaseAndFire(playerSeen);
                }
                else
                {
                    Avoid();
                }
                

                //check for transitions
                if(health.currentHealth < health.maxHealth * 0.5) 
				{
                    ChangeState(AIStates.CheckForFlee);
				}
                
                else if(!PlayerIsInRange(playerSeen)) // if player is no longer in range go back to chasing them
				{
                    ChangeState(AIStates.Chase);
				}
                break;

                //Checks to see if the player is still close if true, continue to flee
            case AIStates.CheckForFlee:
                if (PlayerIsInRange(playerSeen))
                {
                    ChangeState(AIStates.Flee);
                    exitTime = fleeTime;
                }
                else if (!PlayerIsInRange(playerSeen))
                {
                    ChangeState(AIStates.Patrol);
                }
                break;

                //Runs away from the player
            case AIStates.Flee:
                //Do Behaviour

                if (avoidanceStage == AvoidanceStage.NotAvoiding)
                {
                    Flee(playerSeen);
                }
                else
                {
                    Avoid();
                }
                
                //check for transition
                if (exitTime > 0)// this counts down for how long the AI has been fleeing after time is up go back to CheckForFlee
                {
                    exitTime -= Time.deltaTime;
                }
				else
				{
                    ChangeState(AIStates.CheckForFlee);
				}
                break;


            default:
                Debug.LogWarning("[AIPersonality] state doesn't exist");
                break;
		}
    }

	// always runs from the player
	public void CowardlyFSM()
    {
		switch (aiState)
		{
            case AIStates.RotateInPosition: // sits in a spot and spins just checking for the player

                //Do Behavior
                tMotor.Rotate(-tData.rotateSpeed * Time.deltaTime);

                //Check for Transition
                foreach (GameObject player in GameManager.Instance.Players) // checks to see if we can either see or hear the player
                {
                    if (mySenses.CanSee(player))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = player;
                        ChangeState(AIStates.CheckForFlee);

                    }
                    else if (mySenses.CanHear(player))
                    {
                        Debug.Log("I heard something!");
                        playerHeard = player;
                        ChangeState(AIStates.CheckForPlayer);
                        exitTime = timeToCheckForPlayer;
                    }
                }
                break;


            case AIStates.CheckForPlayer: //if the player is heard, turns towards where they heard the player and checks if they can see them

                //Do Behavior
                if (exitTime > 0)
                {
                    tMotor.RotateTowards(playerHeard.transform.position, tData.rotateSpeed);

                //Check for Transition
                    if (mySenses.CanSee(playerHeard))
                    {
                        Debug.Log("I caaaan seeeee yoooou");
                        playerSeen = playerHeard;
                        ChangeState(AIStates.Flee);

                    }
                    exitTime -= Time.deltaTime;
                }
                
                else
				{
                    ChangeState(AIStates.RotateInPosition);
                }
                    
                
                break;


            case AIStates.CheckForFlee: //Checks to see if the player they saw is near them
                if (PlayerIsInRange(playerSeen))
                {
                    ChangeState(AIStates.Flee);
                    exitTime = fleeTime;
                }
                else if (!PlayerIsInRange(playerSeen)) // if the player is no longer near go back to rotate in position
                {
                    ChangeState(AIStates.RotateInPosition);
                }
                break;


            case AIStates.Flee: // Runs away from the player
                //Do Behaviour

                if (avoidanceStage == AvoidanceStage.NotAvoiding)
                {
                    Flee(playerSeen);
                }
                else
                {
                    Avoid();
                }

                //check for transition
                if (exitTime > 0)// this counts down for how long the AI has been fleeing after time is up go back to CheckForFlee
                {
                    exitTime -= Time.deltaTime;
                }
                else
                {
                    ChangeState(AIStates.RotateInPosition);
                }
                break;

            default:
                Debug.LogWarning("[AIPersonality] state doesn't exist");
                break;
        }

    }


                                    // This section is all the methods the AIStates might call

    //Patrols waypoints
    private void Patrol()
	{
        if (isLoopingForward)// going through the waypoints in order
        {
            if (tMotor.RotateTowards(GameManager.Instance.waypoints[currentWaypoint].transform.position, tData.rotateSpeed))
			{
                //do Nothing
			}
            else if (CanMove(checkDistanceAhead))
			{
                //Checks to see if we are close enough to the waypoint
                if (Vector3.SqrMagnitude(transform.position - GameManager.Instance.waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
                {
                    if (currentWaypoint < (GameManager.Instance.waypoints.Count - 1))
                    {
                        //Advance to the next waypoint
                        currentWaypoint++;
                    }
                    else
                    {
                        isLoopingForward = false;
                    }
                }
                else // Move the AI
                {
                    tMotor.Move(tData.moveSpeed);
                }
            }
            else
			{
                avoidanceStage = AvoidanceStage.ObstacleDetected;
			}
            
        }
        else // goes through them in reverse
        {
            if (tMotor.RotateTowards(GameManager.Instance.waypoints[currentWaypoint].transform.position, tData.rotateSpeed))
            {
                //do Nothing
            }
            else if (CanMove(checkDistanceAhead))
			{
                if (Vector3.SqrMagnitude(transform.position - GameManager.Instance.waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
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
    }


    private void RandomPatrol()
    {
        if (tMotor.RotateTowards(GameManager.Instance.waypoints[currentWaypoint].transform.position, tData.rotateSpeed))
		{
            //do nothing
		}
        else if (CanMove(checkDistanceAhead))
		{
            //Checks to see if we are close enough to the waypoint
            if (Vector3.SqrMagnitude(transform.position - GameManager.Instance.waypoints[currentWaypoint].transform.position) <= (closeEnough * closeEnough))
            {
                //Advance to the next random waypoint
                currentWaypoint = UnityEngine.Random.Range(0, GameManager.Instance.waypoints.Count);
            }
            else // Move the AI
            {
                tMotor.Move(tData.moveSpeed);
            }
        }
        else
		{
            avoidanceStage = AvoidanceStage.ObstacleDetected;
		}
        
    }


    //chases the player and also fires at the player
    private void ChaseAndFire(GameObject target)
	{
        //TODO: write this method
        if (tMotor.RotateTowards(target.transform.position, tData.rotateSpeed))
        {
            //Do Nothing
        }
        else
        {
            if (Vector3.SqrMagnitude(transform.position - target.transform.position) >= (closeEnough * closeEnough))
            {
                tMotor.Move(tData.moveSpeed);
                
            }
            tShooter.Shoot();

        }
    }


    //Runs away from the player that was seen
    public void Flee(GameObject player)
    {
        //Grabs vector to our target
        Vector3 vectorToTarget = player.transform.position - transform.position;

        //get vector away from vector
        Vector3 vectorAwayFromTarget = -1 * vectorToTarget;

        //Normalize the vector away from target
        vectorAwayFromTarget.Normalize();

        // Adjust for flee distance
        vectorAwayFromTarget *= fleeDistance;

        //set flee position
        Vector3 fleePosition = vectorAwayFromTarget + transform.position;

        if(CanMove(checkDistanceAhead))
		{
            //StartFleeing 
            tMotor.RotateTowards(fleePosition, tData.rotateSpeed);
            tMotor.Move(tData.moveSpeed);
        }
        else
		{
            avoidanceStage = AvoidanceStage.ObstacleDetected;
		}
        

    }


    //Checks to see if the player is in range
    private bool PlayerIsInRange(GameObject player)
	{
        if(Vector3.SqrMagnitude(player.transform.position - transform.position) <= (enemyRangeForFire * enemyRangeForFire))
		{
            return true;
		}
		else
		{
            return false;
		}
	}


    //Chases the Player if the player is seen
	private void Chase(GameObject target)
	{
        
        if (tMotor.RotateTowards(target.transform.position, tData.rotateSpeed))
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


    //Will attempt to avoid obstacle that are in front of it
    public void Avoid()
    {

        if (avoidanceStage == AvoidanceStage.ObstacleDetected)
        {

            tMotor.Rotate(-tData.rotateSpeed * Time.deltaTime);
            if (CanMove(tData.moveSpeed))
            {
                avoidanceStage = AvoidanceStage.AvoidingObstacle;

                exitTime = avoidanceTime;
            }
        }
        else if (avoidanceStage == AvoidanceStage.AvoidingObstacle)
        {
            //if we can move forward, do so
            if (CanMove(tData.moveSpeed))
            {
                exitTime -= Time.deltaTime;
                tMotor.Move(tData.moveSpeed);

                if (exitTime <= 0)
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


    //Changes the AI state
    void ChangeState(AIStates newState)
	{
        aiState = newState;
        stateExitTime = Time.time;
	}
    

    //Will check to see if there is an obstacle in the way
    private bool CanMove(float checkDistanceAhead)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, checkDistanceAhead))//out: whatever raycast hit we wanna have as an output and assign the values of the raycast into the variable
        {
            if (!hit.collider.CompareTag("Player"))
            {
                //can't move
                return false;
            }
        }
        //otherwise return true
        return true;
    }
}
