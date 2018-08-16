﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinComponent : MonoBehaviour {

	[Range(1, 100)]
	public int coins = 1;
	Animator anim;

	public List<AudioClip> coin_clips;
	[Range(0, 1)] 
	public float vol;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		//combine with other coins
		Vector3 offset = new Vector3(100, 0, 0);
		transform.position += offset;

		RaycastHit2D hit;
		do
		{
			hit = Physics2D.Raycast(
				transform.position - offset, // origin
				Vector2.up, 		// direction!
				0.1f, 		// Only check this cell unit on Grid
				LayerMask.GetMask("Coins"));

			if (hit.collider != null)
			{
				coins += hit.transform.GetComponent<CoinComponent>().coins;
				Destroy(hit.transform.gameObject);
			}
		} while (hit.collider != null);

		transform.position -= offset;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetInteger("Coins", coins);
	}

	public int Coins {
		get { return coins; }
		set { coins = value; }
	}

	public void PlaySound() 
	{
		int idx = Random.Range(0, coin_clips.Count);
		AudioSource.PlayClipAtPoint(coin_clips[idx], transform.position, vol);
	}
}
