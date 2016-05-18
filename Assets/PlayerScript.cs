using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class PlayerScript : MonoBehaviour {

	private Rigidbody rb;
	public int speed;
	PlayerHealth playerhealth;
	GlobalController controller;
	public Canvas tryAgainMenu;
	public Button yesButton;
	public Button noButton;
	GameObject space; 
	private Animator animator;


	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		speed = 200;
		animator = GetComponent<Animator>();
		playerhealth = GetComponent<PlayerHealth> ();
		tryAgainMenu = tryAgainMenu.GetComponent<Canvas> ();
		yesButton = yesButton.GetComponent<Button> ();
		noButton = noButton.GetComponent<Button> ();

		tryAgainMenu.enabled = false;

		
	}
		
	void FixedUpdate () {
		// Keeping the player on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);

		//float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(0.0f, moveVertical, 0.0f);
		if(Input.GetKey(KeyCode.LeftArrow)){
			rotate(8);
		}
		if(Input.GetKey(KeyCode.RightArrow)){
			rotate(-8);
		}

		if(Input.GetKey(KeyCode.UpArrow)){
			forward();
		}else{
			animator.SetTrigger("AstroAnim");
		}
		
		

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
	void rotate(float move){
		transform.Rotate (Vector3.forward * move);
	}

	void forward(){
		animator.SetTrigger ("ThrustAnim");
		rb.AddForce(transform.up * speed * Time.deltaTime);

	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Asteroid")) {
			playerhealth.getHit ();
		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals ("Health")) {
			playerhealth.increaseHealth ();
			Destroy (other.gameObject);
		}
		if(other.gameObject.tag.Equals("Fuel")){
			playerhealth.increaseFuel();
			Destroy(other.gameObject);
			space = GameObject.FindWithTag("Space");
		
			controller=space.GetComponent<GlobalController>();
			controller.pickedUpFuel();
			
		}

	}
}
