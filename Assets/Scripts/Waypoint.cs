﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

	// public ok here as this is a data class
	public bool isExplored = false;
	public Waypoint exploredFrom; 
	public bool isPlaceable = true;

	[SerializeField] GameObject tower;

	Vector2Int gridPos;

	const int gridSize = 10;

	public int GetGridSize()
	{
		return gridSize;
	}

	public Vector2Int GetGridPos()
	{
		return new Vector2Int(
		Mathf.RoundToInt(transform.position.x / gridSize),
        Mathf.RoundToInt(transform.position.z / gridSize)
		);
	}

	void OnMouseOver()
	{
		if (Input.GetButtonDown("Fire1") && isPlaceable)
		{
			Instantiate(tower, transform.position, Quaternion.identity);
			isPlaceable = false;
		}		
	}
}
