using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Astronaut");
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position + offset;
	}
}
