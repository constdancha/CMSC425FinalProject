using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public Text scoreText;
	public Slider health;
	int score;
	// Use this for initialization
	void Start () {
		score=0;
		
		scoreText.text=score.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(health.value>0){
			score+=5;
			scoreText.text=score.ToString();
		}
	}
}
