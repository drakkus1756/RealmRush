using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint;
	[SerializeField] Waypoint endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter;
	List<Waypoint> path = new List<Waypoint>();

	Vector2Int[] directions = {
		Vector2Int.up,
		Vector2Int.right,
		Vector2Int.down,
		Vector2Int.left
		};

	public List<Waypoint> GetPath()
	{
		if (path.Count == 0)
		{
			LoadBlocks();
			BreadthFirstSearch();
			CreatePath();
		}

		return path;
	}

    private void CreatePath()
    {
        path.Add(endWaypoint);

		Waypoint previousWP = endWaypoint.exploredFrom;
		while(previousWP != startWaypoint)
		{
			//add intermediate waypoints
			path.Add(previousWP);
			previousWP = previousWP.exploredFrom;
		}

		path.Add(startWaypoint);
		path.Reverse();
    }

    private void BreadthFirstSearch()
    {
		queue.Enqueue(startWaypoint);

		while(queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			searchCenter.isExplored = true;
			StopIfEndFound();
			ExploreNeighbors();
		}
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
			if (grid.ContainsKey(neighborCoords))
            {
                QueueNewNeighbors(neighborCoords);
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
