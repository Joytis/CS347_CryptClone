﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupWeapon : MonoBehaviour {

	public enum WeaponType {
		DAGGER,
		LONGSWORD,
		GUN
	};

	public GameObject price;
	public GameObject floaty;
	SpriteRenderer floaty_sr;
	public WeaponType type;

	public Sprite dag_sprite;
	public Sprite long_sprite;
	public Sprite gun_sprite;

	void Awake() 
	{
		floaty_sr = floaty.GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () 
	{
		switch(type) {
			case WeaponType.DAGGER:
				floaty_sr.sprite = dag_sprite;
				break;
			case WeaponType.LONGSWORD:
				floaty_sr.sprite = long_sprite;
				break;
			case WeaponType.GUN:
				floaty_sr.sprite = gun_sprite;
				break;
		}	
	}


	void OnTriggerEnter2D(Collider2D other) 
	{
		GameObject obj = other.gameObject;

		if(obj.layer == LayerMask.NameToLayer("Player")){

			PlayerController pc = obj.GetComponent<PlayerController>();

			if(price != null) {

				int obj_price = price.GetComponent<PriceComponent>().GetPrice();
				int coins = obj.GetComponent<PlayerCoinController>().Coins();
				int coin_offset = coins - obj_price;

				if(coin_offset < 0) {
					Debug.Log("Can't pay for item!");
					return;
				}
				// Pay for the cost. 
				obj.GetComponent<PlayerCoinController>().RemoveCoins(obj_price);
				// Get rid of the price component. We'll deal with this later. 
				Destroy(price);
			}

			Debug.Log("Equipping new weapon");

			string ret = "null";
			switch(type) {
				case WeaponType.DAGGER:
					ret = pc.SetWeapon<DaggerWeapon>();
					break;
				case WeaponType.LONGSWORD:
					ret = pc.SetWeapon<LongswordWeapon>();
					break;
				case WeaponType.GUN:
					ret = pc.SetWeapon<GunWeapon>();
					break;
			}

			if(ret == "dagger") {
				type = WeaponType.DAGGER;
				floaty_sr.sprite = dag_sprite;
			}
			else if(ret == "longsword") {
				type = WeaponType.LONGSWORD;
				floaty_sr.sprite = long_sprite;
			}
			else if(ret == "gun") {
				type = WeaponType.GUN;
				floaty_sr.sprite = gun_sprite;
			}
		}
	}
}
