using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	[SerializeField] Transform objectToPan;
	[SerializeField] Transform targetEnemy;

	[SerializeField] float attackRange = 20f;
	[SerializeField] ParticleSystem gun;

	

	// Update is called once per frame
	void Update () 
	{
		CheckIfValidTarget();
	}

    private void CheckIfValidTarget()
    {
        if (targetEnemy)
		{
			FireAtTarget();
		}
		else
		{
			Shoot(false);
		}
    }

    private void FireAtTarget()
    {
        float enemyDistance = Vector3.Distance(targetEnemy.transform.position, gameObject.transform.position);
		
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
