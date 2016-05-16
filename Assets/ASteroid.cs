using UnityEngine;
using System.Collections;

public class ASteroid : MonoBehaviour {
	private GameObject playerObject;
	private Rigidbody rb;
	public int speed = 1;
	public int threshold = 40;
	private bool destroyFlag = false;

	void Start(){
		playerObject = GameObject.Find("Player");
		rb = GetComponent<Rigidbody> ();

		Vector3 movement = (playerObject.transform.position - rb.position);

		movement = movement.normalized * 5;

		movement = new Vector3(movement.x + Random.Range(-5,5), movement.y + Random.Range(-5,5), movement.z);

		movement = movement.normalized * speed;

		// Vector3 movement = new Vector3 (0.0f, -1.0f * speed, 0.0f);
		// rb.AddForce (movement * 1.0f);
		rb.velocity = movement;
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);

		// Keeping asteroids on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);

		if (Vector3.Distance(rb.position, playerObject.transform.position) > threshold) {
			if (destroyFlag) {
				rb.gameObject.SetActive(false);
				Debug.Log("Destroyed " + this);
			}
		} else if (!destroyFlag) {
			destroyFlag = true;
		}
	}
}
