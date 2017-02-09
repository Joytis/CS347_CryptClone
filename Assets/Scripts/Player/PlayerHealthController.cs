﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {

	[Range(1,20)]
	public int STARTING_HEALTH;
	public int heartOffset;
	public GameObject HeartsContainer;
	public GameObject FULL_HEART;
	public GameObject HALF_HEART;
	public GameObject EMPTY_HEART;
	int max_health;
	int health;

	// Use this for initialization
	void Awake () 
	{
		max_health = STARTING_HEALTH + (STARTING_HEALTH % 2); // Make max divisible by 2. 
		health = STARTING_HEALTH;
	}

	void Start() 
	{	
		redrawUIHearts();
	}
	
	public void DealDamage(int dmg) 
	{
		// Recalculate health and UI
		health -= dmg;
		redrawUIHearts();
		if(health <= 0) {
			loseGame();
		}
	}

	void redrawUIHearts() 
	{
		foreach (Transform child in HeartsContainer.transform) {
			Destroy(child.gameObject);
		}

		// Create UI elements 
		GameObject heart;
		int offset;
		for(int i = 0; i < max_health; i += 2) {
			offset = health - (i + 2); // Offset by 2
			if(offset == -1) { // Triggers if a half-heart should be displayed.
				heart = HALF_HEART;
			}
			else if(offset < 0) { // Triggers if <= -2
				heart = EMPTY_HEART;
			}
			else {				// Triggers if posative. 
				heart = FULL_HEART;
			}

			// Create and position object
			GameObject obj = Instantiate(heart);
			obj.transform.SetParent(HeartsContainer.transform, false); // Parent it to UI
			obj.transform.position = HeartsContainer.transform.position
										 + new Vector3((float)(-heartOffset * i), 0f, 0f);
		}
	}

	void loseGame() 
	{
		// LOSE THE GAME HERE.
	}
}