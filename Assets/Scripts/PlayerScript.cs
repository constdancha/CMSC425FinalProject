using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	private Rigidbody rb;
	public ParticleSystem stream;
	public int speed, acceleration;
	PlayerHealth playerhealth;
	GlobalController controller;
	ScoreScript scoreScript;
	public Canvas tryAgainMenu;
	public Canvas pauseMenu;
	public Button yesButton;
	public Button noButton;
	private Animator animator;
	private bool jetSound;
	private bool paused;

	private int endGameTime;
	private float endGameTimer;

	AudioSource audio;
	public AudioClip jetPackSound;
	public AudioClip healthPickUpSound;
	public AudioClip fuelPickUpSound;

	void Start(){
		Time.timeScale = 1;
		rb = GetComponent<Rigidbody> ();
		rb.useGravity = false;
		acceleration = 400;
		speed = 10;
		animator = GetComponent<Animator>();
		playerhealth = GetComponent<PlayerHealth> ();
		scoreScript = GetComponent<ScoreScript>();
		controller = GetComponent<GlobalController>();
		tryAgainMenu = tryAgainMenu.GetComponent<Canvas> ();
		yesButton = yesButton.GetComponent<Button> ();
		noButton = noButton.GetComponent<Button> ();
		audio = GetComponent<AudioSource> ();
		audio.clip = jetPackSound;
		audio.loop = true;
		audio.volume = 0.3F;

		stream.Stop();

		endGameTime = 3;
		endGameTimer = endGameTime;

		paused = false;
		pauseMenu.enabled = false;
		tryAgainMenu.enabled = false;

		jetSound = false;
	}
		
	void Update () {
		// Keeping the player on the z-plane
		rb.position = new Vector3(rb.position.x, rb.position.y, 0);
		if(playerhealth.currentHealth<=0 || playerhealth.fuel<=0){
			audio.Stop();
			stream.Stop();
			animator.SetTrigger("AstroDead");
		}else{
			if(Input.GetKey(KeyCode.LeftArrow)){
				rotate(8);
			}
			if(Input.GetKey(KeyCode.RightArrow)){
				rotate(-8);
			}

			if(Input.GetKeyDown(KeyCode.Escape)){
				paused = !paused;
				pauseMenu.enabled = paused;
				if (paused)
					Time.timeScale = 0;
				else
					Time.timeScale = 1;
			}

			if(Input.GetKey(KeyCode.UpArrow)){
				forward();
				// decrease fuel according to amount of movement
				playerhealth.useFuel();
				if (!jetSound) {
					jetSound = true;
					audio.Play();
					stream.Play();
				}
			}else{
				if(playerhealth.currentHealth>0){
					animator.SetTrigger("AstroAnim");
				}
				audio.Stop();
				stream.Stop();
				if (jetSound)
					jetSound = false;
			}
		}
		
		// Capping player max speed
		if (rb.velocity.magnitude > speed) {
			rb.velocity = rb.velocity.normalized * speed;
		}

		//edit menu
		if(playerhealth.currentHealth <= 0 || playerhealth.fuel <= 0){
			//Debug.Log ("health at 0");
			endGameTimer -= Time.deltaTime;
			if (endGameTimer < 0) {
				tryAgainMenu.enabled = true;
			}
		} else {
			endGameTimer = endGameTime;
		}
	}

	public void unPause() {
		paused = false;
		pauseMenu.enabled = false;
		Time.timeScale = 1;
	}

	public void NoPress(){
		tryAgainMenu.enabled = false;
		SceneManager.LoadScene(0);
	}

	public void YesPress(){
		//load scene again
		// EditorSceneManager.LoadScene("FinalProject_Daniel");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
			other.gameObject.GetComponent<AudioSource>().PlayOneShot(healthPickUpSound, 1f);
			playerhealth.increaseHealth ();
			scoreScript.increaseScore(50);
			other.gameObject.transform.position += Vector3.back * 80;
			Destroy (other.gameObject, 2.0f);
		}
		if(other.gameObject.tag.Equals("Fuel")){
			playerhealth.increaseFuel();
			other.gameObject.GetComponent<AudioSource>().PlayOneShot(fuelPickUpSound, 1f);
			other.gameObject.transform.position += Vector3.back * 80;
			Destroy(other.gameObject, 2.0f);

			scoreScript.increaseScore(100);
		
			controller.pickedUpFuel();
			
		}

	}
}
