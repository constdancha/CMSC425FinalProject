﻿using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

	public float interval;
	private float timeLeft;
	public GameObject[] asteroidPrefabs;
	public GameObject asteroidParent;
	private GameObject tempAsteroid;
	public GameObject playerObject;
	public GameObject FuelPrefab;
	public GameObject HealthPrefab;
	public GameObject arrow;
	private GameObject fuel;
	
	int numfuel=0;
	public int startingAsteroids = 100;
	public int startingHealths = 10;
	public float fuelRange = 5;
	public float dimX, dimY;

	// Use this for initialization
	void Start () {
		timeLeft = interval;

		Renderer spaceRenderer = GetComponent<Renderer>();
		dimX = spaceRenderer.bounds.size.x;
		dimY = spaceRenderer.bounds.size.y;

		Debug.Log(dimX + " " + dimY);

		for (int i = 0; i < startingAsteroids; i++) {
			Vector3 startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);
			
			// Make sure nothing spawns on the player
			while (Vector3.Distance(startingPosition, playerObject.transform.position) < 5)
				startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);

			createAsteroid(startingPosition);
		}

		for (int i = 0; i < startingHealths; i++) {
			Vector3 startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);
			createHealthPickup(startingPosition);
		}

		fuel = createFuel(playerObject.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Spawn new fuel pickup if one doesn't exist
		if(numfuel==0){
			fuel = createFuel(playerObject.transform.position);
		}

		// generate new asteroids
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = interval;

			float random = Random.Range(0, 4);
			float x = 0, y = 0;
			if (random < 1) {
				x = -0.2f;
				y = Random.Range(0, 1);
			} else if (random < 2) {
				x = 1.2f;
				y = Random.Range(0, 1);
			} else if (random < 3) {
				x = Random.Range(0, 1);
				y = -0.2f;
			} else {
				x = Random.Range(0, 1);
				y = 1.2f;
			}
			Vector3 startingPosition = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));

			createAsteroid(startingPosition);
		}

		// Update arrow position
		Vector3 fuelCameraPosition = Camera.main.WorldToViewportPoint(fuel.transform.position);
		if (fuel && !(fuelCameraPosition.x < 1 && fuelCameraPosition.x > 0 && fuelCameraPosition.y < 1 && fuelCameraPosition.y > 0)) {
			arrow.SetActive(true);

			// position arrow
			Vector3 arrowPos = new Vector3(0.5f, 0.5f, 0) - (playerObject.transform.position - fuel.transform.position).normalized*0.4f;
			arrowPos = Camera.main.ViewportToWorldPoint(arrowPos);
			arrowPos = new Vector3(arrowPos.x, arrowPos.y, 0);
			arrow.transform.position = arrowPos;

			// arrow.transform.position = playerObject.transform.position + arrowPos * 5;

			// rotate arrow
			Vector3 target = fuel.transform.position - playerObject.transform.position;
			float angle = Mathf.Atan2(target.y, target.x);
			arrow.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle - 90, Vector3.forward);
		} else {
			arrow.SetActive(false);
		}
	}

	void createAsteroid(Vector3 position) {
		tempAsteroid = asteroidPrefabs[(int)Mathf.Round(Random.Range(0,2))];

		GameObject asteroid = (GameObject)Instantiate(
        		tempAsteroid,
        		position,
        		Quaternion.identity
        		);

		asteroid.transform.parent = asteroidParent.transform;
	}

	GameObject createFuel(Vector3 position){
		numfuel=1;
		Vector3 startingPosition = new Vector3 (
                Random.Range(-10, 10),
                Random.Range(-10, 10),
                0
                );
		startingPosition = position + startingPosition.normalized * fuelRange;
		GameObject fuel = (GameObject)Instantiate(
        		FuelPrefab,
        		startingPosition,
        		Quaternion.identity
        		);
		return fuel;
	}

	void createHealthPickup(Vector3 position){
		GameObject fuel = (GameObject)Instantiate(
        		HealthPrefab,
        		position,
        		Quaternion.identity
        		);
	}

	public void pickedUpFuel(){
		numfuel=0;
	}
}
