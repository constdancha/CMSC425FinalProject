using UnityEngine;
using System.Collections;

public class ASteroid : MonoBehaviour {
	private GameObject playerObject;
	private Rigidbody rb;
	public int speed = 1;
	public int threshold = 100;
	public int cameraThreshold;

	AudioSource audio;
	public AudioClip asteroidHitSound;

	void Start(){
		playerObject = GameObject.Find("Astronaut");
		rb = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();

		Vector3 movement = (playerObject.transform.position - rb.position);

		movement = movement.normalized * 5;

		movement = new Vector3(movement.x + Random.Range(-5,5), movement.y + Random.Range(-5,5), movement.z);

		movement = movement.normalized * speed;

		// Vector3 movement = new Vector3 (0.0f, -1.0f * speed, 0.0f);
		// rb.AddForce (movement * 1.0f);
		rb.velocity = movement;

		GameObject cameraObject = GameObject.Find("Main Camera");
		Camera camera = cameraObject.GetComponent<Camera>();
		cameraThreshold = camera.pixelWidth;
		// cameraThreshold = Screen.width;
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (0, 0, Random.Range(5, 25)) * Time.deltaTime);

		// Keeping asteroids on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);
	}

	void OnCollisionEnter(Collision collision){
		// Bouncing off of collided object
		Vector3 movement = (rb.position - collision.gameObject.transform.position).normalized * speed;
		rb.velocity = movement;

		// Making collision noises
		if (collision.gameObject.tag.Equals ("Player")) {
			audio.PlayOneShot (asteroidHitSound, 1F);
		} else if (collision.gameObject.tag.Equals ("Asteroid")) {
			// Only play collision sound if close to player
			// if (Vector3.Distance(rb.position, playerObject.transform.position) < cameraThreshold) {
			// 	audio.PlayOneShot (asteroidHitSound, 0.2F);
			// 	Debug.Log(cameraThreshold);
			// }
		}
	}
}
