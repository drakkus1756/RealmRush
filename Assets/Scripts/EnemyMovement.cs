using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    int hits = 10;

	// Use this for initialization
	void Start ()
    {
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
    }

    void OnParticleCollision(GameObject other)
    {
        print("Particle collision");
        hits--;
        
        if (hits < 1)
        {
            Destroy(gameObject);
        }
    }
}
