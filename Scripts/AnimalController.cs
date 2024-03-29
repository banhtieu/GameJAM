﻿using UnityEngine;
using System.Collections;

public class AnimalController : BaseBehavior {

	// maximum health
	public int maxHP = 100;

	// health drain speed per second
	public int drainSpeed = 0;

	// current health of this object
	public float health;

	// the image of this animal
	public Sprite image;

	// The introduction
	public string introduce;

	// heart particle
	public GameObject heartParticle;

	// crying animal
	public GameObject cryingAnimal;

	public Color fullColor = Color.green;

	public Color deadColor = Color.red;

	// number of bars counts
	public const int BARS_COUNT = 3;

	// the spawner
	private AnimalSpawner spawner;
	
	// health bar
	private GameObject healthBar;

	// On Mouse Down
	void OnMouseDown() {
		GameController.GetInstance().ShowInfo(introduce, image);
	}

	// Use this for initialization
	void Start () {
		if (spawner != null) {
			health = maxHP;
		}

		GameObject bar = transform.Find("HealthBar").gameObject;
		// get health bars
		healthBar = bar.transform.Find("Health").gameObject;
	}


	// Update is called once per frame
	public override void ConditionalUpdate () {
		if (health > 0) {
			health -= drainSpeed * Time.deltaTime;
		}

		// this die
		if (health <= 0 && spawner != null) {
			// die -- game over
			GameController.GetInstance().GameOver();
			Instantiate(cryingAnimal, transform.position, transform.rotation);
			DestroyObject(gameObject);
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


			Instantiate(heartParticle, transform.position, transform.rotation);

			if (spawner != null) {
				gameObject.GetComponent<Animator>().Play("AnimalFull");
				spawner.ReportFeed(gameObject);
				DestroyObject(gameObject, 1.0f);
				drainSpeed = 0;
			} else {
				health = 0;
			}


		}

		UpdateHealthBar();

		return isFull;

	}

	// set current health
	void UpdateHealthBar() {
		float scale = (health / (maxHP));
		healthBar.transform.localScale = new Vector3(scale, 1.0f, 1.0f);
		healthBar.GetComponent<SpriteRenderer>().color = Color.Lerp(deadColor, fullColor, scale);
	}

	public void SetSpawner(AnimalSpawner spawner) {
		this.spawner = spawner;
	}
}
