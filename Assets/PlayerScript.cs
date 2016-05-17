using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//need speed, health

	private Rigidbody rb;
	public int speed;
	PlayerHealth playerhealth;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		speed = 200;

		playerhealth = GetComponent<PlayerHealth> ();
	}
		
	void FixedUpdate () {
		// Keeping the player on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
		rb.AddForce(movement * speed * Time.deltaTime);

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Asteroid")) {
			playerhealth.getHit ();
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals ("Health")) {
			playerhealth.increaseHealth ();
		}
		Destroy (other.gameObject);
	}
}
