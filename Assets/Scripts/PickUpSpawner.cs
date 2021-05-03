using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{

    public GameObject[] pickUpToSpawnIn;
    public GameObject spawnedPickup;
    public float secondUntilSpawn = 30f;
    private float secondsRemaining;
	private int randIndex;
    // Start is called before the first frame update
    void Start()
	{
		SpawnPickUp();
	}

	// Update is called once per frame
	void Update()
	{
		//will check to see if there is something spawned. if there's nothing there start counting down respawn time then spawn something
		if(spawnedPickup == null)
		{
			secondsRemaining -= Time.deltaTime;
			if (secondsRemaining <= 0)
			{
				SpawnPickUp();
			}
		}
	}

	public void SpawnPickUp()
	{
		randIndex = Random.Range(0, pickUpToSpawnIn.Length);// gets a random number 
		//Spawns in powerUp
		spawnedPickup = Instantiate(pickUpToSpawnIn[randIndex], transform.position, Quaternion.identity);//instantiates a random power up
		//reset timer
		secondsRemaining = secondUntilSpawn;//resets respawn timer
	}

	
}
