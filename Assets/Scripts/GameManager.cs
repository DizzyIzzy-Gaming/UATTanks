using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
	public GameObject playerPrefab;//houses player prefab
	public GameObject[] EnemyAI;//List of enemy AI in the scene
	public GameObject[] Players = new GameObject[2];
	public List<GameObject> waypoints;//list of waypoints
	public List<GameObject> healthPowerUps = new List<GameObject>();// list of power ups
	public GameObject[] EnemyAIPrefabs;// list of AI prefabs

	public int oldPlayerScore;

	public ScoreData player1Score;
	public ScoreData player2Score;

	public List<EnemySpawnPoints> enemySpawnPoints = new List<EnemySpawnPoints>();//list of enemy spawn points
	public EnemySpawnPoints lastPointSpawnedOn;// a variable to hold the last enemy spawnpoint that was used
	public List<PlayerSpawnPoints> playerSpawnPoints = new List<PlayerSpawnPoints>();

	public enum MapGenerationType { Random, MapOfTheDay, CustomSeed };
	public MapGenerationType mapType = MapGenerationType.Random;

	public float musicVolume;
	public float sfxVolume;

	// Start is called before the first frame update
	protected override void Awake()
	{
		
		LoadPreferences();
		base.Awake();
	}
	private void Start()
	{
		SceneManager.LoadScene(1);
	}

	public void SpawnEnemies(int numberToSpawn)
	{ 

		for (int enemy = 0; enemy < numberToSpawn; enemy++)
		{
			EnemySpawnPoints randomSpawnPoint = enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Count)];

			//tries to keep them from spawning on each other
			if(lastPointSpawnedOn == randomSpawnPoint)// if the waypoint we randomly selected picks the waypoint we just last selected try again
			{
				enemy--;
			}
			else
			{
				randomSpawnPoint.SpawnRandomEnemy();
				lastPointSpawnedOn = randomSpawnPoint;//assigned the spawnpoint we just used to last points Spawned on 
			}
			
		}
	}
	public void SpawnPlayers()// grabs a random spawn point and tells it to spawn the player
	{
		PlayerSpawnPoints randomPlayerSpawnPoint = playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Count)];
		randomPlayerSpawnPoint.SpawnPlayer();
	}

	public void SavePreferences()
	{
		PlayerPrefs.SetFloat("musicVolume", musicVolume);
		PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
		//TODO: test this out
		PlayerPrefs.SetInt("mapType", (int) mapType);
		PlayerPrefs.Save();
	}

	public void LoadPreferences()
	{
		if(PlayerPrefs.HasKey("musicVolume"))
		{
			musicVolume = PlayerPrefs.GetFloat("musicVolume");
		}
		else
		{
			sfxVolume = 1.0f;
		}

		if (PlayerPrefs.HasKey("sfxVolume"))
		{
			sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
		}
		else
		{
			sfxVolume = 1.0f;
		}

		if(PlayerPrefs.HasKey("mapType"))
		{
			mapType = (MapGenerationType) PlayerPrefs.GetInt("mapType");
		}
		else
		{
			mapType = MapGenerationType.Random;
		}
		//Load Music volume
	}
	
}
