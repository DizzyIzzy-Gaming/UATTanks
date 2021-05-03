using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoints : MonoBehaviour
{
	private Room myParentRoom;

	private void Awake()
	{

		GameManager.Instance.enemySpawnPoints.Add(this);
		myParentRoom = GetComponentInParent<Room>();//grabs the waypoints parent room component
		
	}

	private void OnDestroy()
	{
		//if the game manager doesnt exist we cant access it
		if(GameManager.Instance == null)
		{
			return;
		}
		//remove from list if we are on the list
		if (GameManager.Instance.enemySpawnPoints.Contains(this))
		{
			GameManager.Instance.enemySpawnPoints.Remove(this);
		}
	}
	public void SpawnRandomEnemy()
	{
		// select a random enemy to spawn
		GameObject prefabToSpawn = GameManager.Instance.EnemyAIPrefabs[Random.Range(0, GameManager.Instance.EnemyAIPrefabs.Length)];

		GameObject spawnedEnemy = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);

		//WIP: I am trying to have the AI assigned the waypoints of it room and get them to patrol a specific area
		foreach(GameObject waypoint in myParentRoom.wayPoints)
		{
			spawnedEnemy.GetComponent<AIPersonalities>().myWaypoints.Add(waypoint);
		}
		
	}
}