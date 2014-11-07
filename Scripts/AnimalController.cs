﻿using UnityEngine;
using System.Collections;

public class AnimalController : BaseBehavior {

	// maximum health
	public int maxHP = 100;

	// health drain speed per second
	public int drainSpeed = 0;

	// current health of this object
	public float health;

	// number of bars counts
	public const int BARS_COUNT = 3;

	// health bar
	private GameObject[] healthItems = new GameObject[BARS_COUNT];
	
	// Use this for initialization
	void Start () {
		health = maxHP;

		GameObject healthBar = transform.Find("HealthBar").gameObject;
		// get health bars
		for (int i = 0; i < BARS_COUNT; i++) {
			Debug.Log("Health" + i);
			healthItems[i] = healthBar.transform.Find("Health" + i).gameObject;
		}
	}
	
	// Update is called once per frame
	public override void ConditionalUpdate () {
		if (health > 0) {
			health -= drainSpeed * Time.deltaTime;
		}

		// this die
		if (health <= 0) {
			// die -- game over
		}

		UpdateHealthBar();
	}

	// feeding 
	public bool Feed(float food) {
		bool isFull = false;
		health += food;
		if (health >= (float) maxHP) {
			health = maxHP;
			isFull = true;
		}

		UpdateHealthBar();

		return isFull;

	}

	// set current health
	void UpdateHealthBar() {
		// update health bars
		for (int i = 0; i < BARS_COUNT; i++) {
			float opacity = (health - i * (maxHP / 3)) / (maxHP / 3);
			opacity = Mathf.Clamp(opacity, 0.0f, 1.0f);
			healthItems[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, opacity);
		}
	}
}