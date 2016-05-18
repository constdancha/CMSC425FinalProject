using UnityEngine;
using System.Collections;

public class SpaceController : MonoBehaviour {

	GlobalController controller;
	public int startingAsteroids = 100;
	public int startingHealths = 10;
	float dimX, dimY;
	Vector3 center;
	Bounds bounds;

	bool left, right, up, down, up_right, up_left, down_right, down_left;

	private GameObject player;

	// Use this for initialization
	void Start () {
		Renderer spaceRenderer = GetComponent<Renderer>();
		bounds = new Bounds(spaceRenderer.bounds.center, spaceRenderer.bounds.size+Vector3.forward*120);
		dimX = spaceRenderer.bounds.size.x;
		dimY = spaceRenderer.bounds.size.y;
		center = spaceRenderer.bounds.center;

		left = false;
		right = false;
		up = false;
		down = false;
		up_right = false;
		down_right = false;
		up_left = false;
		down_left = false;

		player = GameObject.Find("Astronaut");
		controller = player.GetComponent<GlobalController>();

		for (int i = 0; i < startingAsteroids; i++) {
			Vector3 startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);
			
			// Make sure nothing spawns on the player
			while (Vector3.Distance(startingPosition, player.transform.position) < 5)
				startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);

			controller.createAsteroid(new Vector3(center.x, center.y, 0)+startingPosition);
		}

		for (int i = 0; i < startingHealths; i++) {
			Vector3 startingPosition = new Vector3(Random.Range(-dimX/2, dimX/2), Random.Range(-dimY/2, dimY/2), 0);
			controller.createHealthPickup(new Vector3(center.x, center.y, 0)+startingPosition);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bounds.Contains(player.transform.position)) {
			if (!up_right && player.transform.position.x > center.x+dimX/4 && player.transform.position.y > center.y+dimY/4) {
				controller.createSpace(center + new Vector3(dimX, dimY, 0), this.transform.rotation);
				up_right = true;
			}

			if (!up_left && player.transform.position.x < center.x-dimX/4 && player.transform.position.y > center.y+dimY/4) {
				controller.createSpace(center + new Vector3(-dimX, dimY, 0), this.transform.rotation);
				up_left = true;
			}

			if (!down_right && player.transform.position.y < center.y-dimY/4 && player.transform.position.x > center.x+dimX/4) {
				controller.createSpace(center + new Vector3(dimX, -dimY, 0), this.transform.rotation);
				down_right = true;
			}

			if (!down_left && player.transform.position.y < center.y-dimY/4 && player.transform.position.x < center.x-dimX/4) {
				controller.createSpace(center + new Vector3(-dimX, -dimY, 0), this.transform.rotation);
				down_left = true;
			}

			if (!right && player.transform.position.x > center.x+dimX/4) {
				controller.createSpace(center + new Vector3(dimX, 0, 0), this.transform.rotation);
				right = true;
			}

			if (!left && player.transform.position.x < center.x-dimX/4) {
				controller.createSpace(center - new Vector3(dimX, 0, 0), this.transform.rotation);
				left = true;
			}

			if (!up && player.transform.position.y > center.y+dimY/4) {
				controller.createSpace(center + new Vector3(0, dimY, 0), this.transform.rotation);
				up = true;
			}

			if (!down && player.transform.position.y < center.y-dimY/4) {
				controller.createSpace(center - new Vector3(0, dimY, 0), this.transform.rotation);
				down = true;
			}
		}
	}
}
