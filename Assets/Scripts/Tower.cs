using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[SerializeField] Transform objectToPan;
	[SerializeField] float attackRange = 20f;
	[SerializeField] ParticleSystem gun;

	[SerializeField] Transform parent;

	Transform targetEnemy;

	// Update is called once per frame
	void Update () 
	{
		SetTargetEnemy();
		if (targetEnemy)
        {
            FireAtTarget();
        }
        else
        {
            Shoot(false);
        }
	}

    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>();
		if (sceneEnemies.Length == 0) { return; }

		Transform closestEnemy = sceneEnemies[0].transform;

		foreach(EnemyDamage testEnemy in sceneEnemies)
		{
			closestEnemy = GetClosestEnemy(closestEnemy, testEnemy.transform);
		}
		targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform transformA, Transform transformB)
    {
        var distanceToA = Vector3.Distance(transformA.position, transform.position);
		var distanceToB = Vector3.Distance(transformB.position, transform.position);

		if (distanceToA < distanceToB)
		{
			return transformA;
		}

		return transformB;
    }

    private void FireAtTarget()
    {
        float enemyDistance = Vector3.Distance(targetEnemy.position, transform.position);
		
		if (enemyDistance <= attackRange)
		{
			objectToPan.LookAt(targetEnemy);
			Shoot(true);
		}
		else
		{
			Shoot(false);
		}
    }

    private void Shoot(bool isActive)
    {
        var emissionModule = gun.emission;
        emissionModule.enabled = isActive;
	}
}
