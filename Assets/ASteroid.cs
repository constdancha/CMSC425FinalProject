using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	private Rigidbody rb;
	public int speed = 1;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		Vector3 movement = new Vector3 (0.0f, -1.0f * speed, 0.0f);
		rb.AddForce (movement * 1.0f);

		if (rb.position.y < -10) {
			rb.gameObject.SetActive(false);
			Debug.Log(this);
			Destroy(this);
		}
	}
}
