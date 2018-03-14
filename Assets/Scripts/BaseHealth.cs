using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour {

	int health = 25;
	Text healthText;

	// Use this for initialization
	void Start () 
	{
		healthText = GetComponent<Text>();
		healthText.text = health.ToString();

	}
	
	public void AtBase (int DamagePerMinion)
	{
		health = health - DamagePerMinion;
		healthText.text = health.ToString();
	}
}
