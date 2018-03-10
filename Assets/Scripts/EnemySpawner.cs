using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[Tooltip("in seconds")][SerializeField] float spawnDelay = 5f;

	[SerializeField] GameObject enemyPrefab;
	[SerializeField] Transform parent;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(SpawnEnemy());
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	IEnumerator SpawnEnemy()
	{
		yield return new WaitForSeconds(spawnDelay);
		GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		newEnemy.transform.parent = parent;
		
		StartCoroutine(SpawnEnemy());
	}
}
