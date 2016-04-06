using UnityEngine;
using System.Collections;

public class ASteroid : MonoBehaviour {

	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
		Vector3 movement = new Vector3 (0.0f, -1.0f, 0.0f);
		rb.AddForce (movement * 1.0f);
	}
}
