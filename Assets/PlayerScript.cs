using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class PlayerScript : MonoBehaviour {

	private Rigidbody rb;
	public int speed, acceleration;
	PlayerHealth playerhealth;
	GlobalController controller;
	public Canvas tryAgainMenu;
	public Button yesButton;
	public Button noButton;
	GameObject space; 
	private Animator animator;
	private bool jetSound;

	AudioSource audio;
	public AudioClip jetPackSound;
	public AudioClip healthPickUpSound;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		acceleration = 400;
		speed = 10;
		animator = GetComponent<Animator>();
		playerhealth = GetComponent<PlayerHealth> ();
		tryAgainMenu = tryAgainMenu.GetComponent<Canvas> ();
		yesButton = yesButton.GetComponent<Button> ();
		noButton = noButton.GetComponent<Button> ();
		audio = GetComponent<AudioSource> ();
		audio.clip = jetPackSound;
		audio.loop = true;
		audio.volume = 0.3F;

		tryAgainMenu.enabled = false;

		jetSound = false;
	}
		
	void FixedUpdate () {
		// Keeping the player on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);
		float moveVertical = Input.GetAxis("Vertical");
		Vector3 movement = new Vector3(0.0f, moveVertical, 0.0f);
		if(playerhealth.currentHealth<=0 || playerhealth.fuel<=0){
			animator.SetTrigger("AstroDead");
		}else{			
			if(Input.GetKey(KeyCode.LeftArrow)){
				rotate(8);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				rotate(-8);
			}

			if(Input.GetKey(KeyCode.UpArrow)){
				forward();
				if (!jetSound) {
					jetSound = true;
					audio.Play();
				}
			}else{
				if(playerhealth.currentHealth>0){
					animator.SetTrigger("AstroAnim");
					audio.Stop();
				}
				if (jetSound)
					jetSound = false;
			}
		}
		
		// Capping player max speed
		if (rb.velocity.magnitude > speed) {
			Debug.Log("too fast!");
			rb.velocity = rb.velocity.normalized * speed;
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
		rb.AddForce(transform.up * acceleration * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Asteroid")) {
			playerhealth.getHit();

		}
	}
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals ("Health")) {
			audio.PlayOneShot (healthPickUpSound, 0.9F);
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
