﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour {

	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();

	// Use this for initialization
	void Start () 
	{
		ColorWaypoints();
		LoadBlocks();
	}

    private void ColorWaypoints()
    {
        startWaypoint.SetTopColor(Color.green);
		endWaypoint.SetTopColor(Color.blue);
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
