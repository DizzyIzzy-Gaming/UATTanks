using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameObject playerPrefab;//houses player prefab
	public GameObject[] EnemyAI;//List of enemy AI in the scene
	// Start is called before the first frame update
	protected override void Awake()
	{
		base.Awake();
	}
}
