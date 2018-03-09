using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;

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
			var searchCenter = queue.Dequeue();
			searchCenter.isExplored = true;
			print("Searching from " + searchCenter); // TODO: remove
			StopIfEndFound(searchCenter);
			ExploreNeighbors(searchCenter);
		}
		// TODO: workout path
		print("Finished pathfinding?");
    }

    private void StopIfEndFound(Waypoint searchCenter)
    {
        if (searchCenter == endWaypoint)
		{
			print("Searching from end node, stopping search");
			isRunning = false;
		}
    }

    private void ExploreNeighbors(Waypoint from)
    {
		if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
		{
			Vector2Int neighborCoords = from.GetGridPos() + direction;
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
		if (neighbor.isExplored)
		{
			// do nothing
		}
		else
		{
			neighbor.SetTopColor(Color.cyan); // TODO: move later
        	queue.Enqueue(neighbor);
        	print("Queuing " + neighbor);
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
