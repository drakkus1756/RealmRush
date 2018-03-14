using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[Range(0.1f, 120.0f)][SerializeField] float spawnDelay = 5f;
	[SerializeField] GameObject enemyPrefab;
	[SerializeField] Transform parent;

	GameObject spawnedEnemy;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(SpawnEnemy());
	}

	IEnumerator SpawnEnemy()
	{
		while (true) // forever
		{
			spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity) as GameObject;
			spawnedEnemy.transform.parent = parent;
			yield return new WaitForSeconds(spawnDelay);
		}
		
	}
}
