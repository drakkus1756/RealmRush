using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter; // current search center

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left };

	// Use this for initialization
	void Start () 
	{
		ColorWaypoints();
		LoadBlocks();
		Pathfind();
		//ExploreNeighbors();
	}

    private void Pathfind()
    {
		queue.Enqueue(startWaypoint);

		while(queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			searchCenter.isExplored = true;
			StopIfEndFound();
			ExploreNeighbors();
		}
		// TODO: workout path
		print("Finished pathfinding?");
    }

    private void StopIfEndFound()
    {
        if (searchCenter == endWaypoint)
		{
			isRunning = false;
		}
    }

    private void ExploreNeighbors()
    {
		if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
		{
			Vector2Int neighborCoords = searchCenter.GetGridPos() + direction;
			try
            {
                QueueNewNeighbors(neighborCoords);
            }
            catch
			{
				// do nothing
			}
			
		}
    }

    private void QueueNewNeighbors(Vector2Int neighborCoords)
    {
        Waypoint neighbor = grid[neighborCoords];
		if (neighbor.isExplored || queue.Contains(neighbor))
		{
			// do nothing
		}
		else
		{
        	queue.Enqueue(neighbor);
			neighbor.exploredFrom = searchCenter;
		}

    }

    private void ColorWaypoints()
    {
        startWaypoint.SetTopColor(Color.green);
		endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
		foreach (Waypoint waypoint in waypoints)
		{
			var gridPos = waypoint.GetGridPos();
			if (grid.ContainsKey(gridPos))
			{
				Debug.LogWarning("Skipping overlapping block " + waypoint);
			}
			else
			{
				grid.Add(gridPos, waypoint);
			}
		}
    }
}
