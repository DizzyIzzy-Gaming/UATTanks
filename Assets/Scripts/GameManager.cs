using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameObject playerPrefab;//houses player prefab
	public GameObject[] EnemyAI;//List of enemy AI in the scene
	public GameObject[] Players = new GameObject[2];
	public List<GameObject> waypoints;

	// Start is called before the first frame update
	protected override void Awake()
	{
		base.Awake();
	}
}
