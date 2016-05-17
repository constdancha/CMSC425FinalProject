using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class PlayerScript : MonoBehaviour {

	private Rigidbody rb;
	public int speed;
	PlayerHealth playerhealth;
	public Canvas tryAgainMenu;
	public Button yesButton;
	public Button noButton;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		speed = 200;

		playerhealth = GetComponent<PlayerHealth> ();
		tryAgainMenu = tryAgainMenu.GetComponent<Canvas> ();
		yesButton = yesButton.GetComponent<Button> ();
		noButton = noButton.GetComponent<Button> ();

		tryAgainMenu.enabled = false;
	}
		
	void FixedUpdate () {
		// Keeping the player on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
		rb.AddForce(movement * speed * Time.deltaTime);

		//edit menu
		if(playerhealth.currentHealth <= 0 || playerhealth.fuel <= 0){
			//Debug.Log ("health at 0");
			tryAgainMenu.enabled = true;
		}

		// decrease fuel according to amount of movement
		if (movement.magnitude > 0) {
			playerhealth.useFuel();
		}
	}

	public void NoPress(){
		tryAgainMenu.enabled = false;
		Application.Quit ();
	}

	public void YesPress(){
		//load scene again
		EditorSceneManager.LoadScene("OpeningScene_Ella2");
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
