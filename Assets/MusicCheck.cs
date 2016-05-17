using UnityEngine;
using System.Collections;

public class MusicCheck : MonoBehaviour {

	public GameObject music;
	// Use this for initialization
	void Start () {
		GameObject musicObject = GameObject.FindGameObjectWithTag ("GameController");
		if(!musicObject){
			Instantiate (music, Vector3.zero, Quaternion.identity);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
