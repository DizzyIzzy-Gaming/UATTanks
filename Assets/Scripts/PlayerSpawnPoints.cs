using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPoints : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameManager.Instance.playerSpawnPoints.Add(this);
		
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDestroy()
	{
		//if the game manager doesnt exist we cant access it
		if (GameManager.Instance == null)
		{
			return;
		}
		//remove from list if we are on the list
		if (GameManager.Instance.playerSpawnPoints.Contains(this))
		{
			GameManager.Instance.playerSpawnPoints.Remove(this);
		}
	}
	public void SpawnPlayer()
	{
		// select a random enemy to spawn
		GameObject prefabToSpawn = GameManager.Instance.playerPrefab;

		GameObject spawnedPlayer = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
		GameManager.Instance.Players[0] = spawnedPlayer;
	}
}
