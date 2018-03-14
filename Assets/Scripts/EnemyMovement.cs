using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    BaseHealth baseHealth;
	int DamagePerMinion = 1;

	// Use this for initialization
	void Start ()
    {
        baseHealth = FindObjectOfType<BaseHealth>();
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
		print("Starting patrol...");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
			yield return new WaitForSeconds(1f);
        }
        print("Ending patrol...");
        baseHealth.AtBase(DamagePerMinion);
        Destroy(gameObject);
    }
}
