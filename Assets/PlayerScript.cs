using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//need speed, health

	private Rigidbody rb;
	public int speed;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		speed = 200;
	}
		
	void FixedUpdate () {
		// Keeping the player on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
		rb.AddForce(movement * speed * Time.deltaTime);
	
	}
}
