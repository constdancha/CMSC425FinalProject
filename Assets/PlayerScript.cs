using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	//need speed, health

	private Rigidbody rb;
	public int speed;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		speed = 50;
	}
		
	void FixedUpdate () {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 leftForce, rightForce, downForce, upForce;
		int opposingForce = 5;

		if (rb.position.x >= 8.70f) {
			leftForce = new Vector3 (-3.0f, 0.0f, 0.0f);
			rb.AddForce (leftForce * opposingForce);

		} else if(rb.position.x <= -8.70f){
			rightForce = new Vector3 (3.0f, 0.0f, 0.0f);
			rb.AddForce (rightForce * opposingForce);

		}else if(rb.position.y >= 4.40f){
			downForce = new Vector3 (0.0f, -3.0f, 0.0f);
			rb.AddForce (downForce * opposingForce);

		}else if(rb.position.y <= -4.40f){
			upForce = new Vector3 (0.0f, 3.0f, 0.0f);
			rb.AddForce (upForce * opposingForce);
		}

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);
		rb.AddForce (movement * speed * Time.deltaTime);
	
	}
}
