using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public Text scoreText;
	int score;
	// Use this for initialization
	void Start () {
		score=0;
		
		scoreText.text=score.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		score+=5;
		scoreText.text=score.ToString();
	}
}
