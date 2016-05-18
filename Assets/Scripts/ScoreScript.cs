using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {
	public Text scoreText;
	public Slider health;
	public Slider fuel;

	private int interval;
	private float time;

	int score;
	// Use this for initialization
	void Start () {
		score=0;
		interval=1;
		time = interval;
		
		scoreText.text=score.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(health.value>0 && fuel.value>0){
			time -= Time.deltaTime;
			if (time < 0) {
				time = interval;
				score += 5;
			}
			scoreText.text=score.ToString();
		}
	}

	public void increaseScore(int num) {
		score += num;
	}
}
