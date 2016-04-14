using UnityEngine;
using System.Collections;

public class GlobalController : MonoBehaviour {

	public float interval;
	private float timeLeft;
	public GameObject[] asteroidPrefabs;
	public GameObject asteroidParent;
	private GameObject tempAsteroid;

	// Use this for initialization
	void Start () {
		timeLeft = interval;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timeLeft -= Time.deltaTime;
		if (timeLeft < 0) {
			timeLeft = interval;
			createAsteroid();
		}	
	}

	void createAsteroid() {
		// Debug.Log("Asteroid created!");
		Vector3 startingPosition = new Vector3 (
                Random.Range(-14, 5),
                6f,
                0
                );

		Debug.Log(startingPosition.x);

		tempAsteroid = asteroidPrefabs[(int)Mathf.Round(Random.Range(0,2))];

		GameObject asteroid = (GameObject)Instantiate(
        		tempAsteroid,
        		startingPosition,
        		Quaternion.identity
        		);

		asteroid.transform.parent = asteroidParent.transform;
	}
}
