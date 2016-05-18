using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class menuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
	float interval;
	GameObject tempAsteroid;
	public GameObject[] asteroidPrefabs;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();

		quitMenu.enabled = false;

		interval = Random.Range(2, 10);
	}

	public void ExitPress() {
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress(){
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel(){
		//go to game scene
		SceneManager.LoadScene(1);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	// Update is called once per frame
	void Update () {
		interval -= Time.deltaTime;
		if (interval < 0) {
			tempAsteroid = asteroidPrefabs[(int)Mathf.Round(Random.Range(0,2))];

			interval = Random.Range(2, 10);
			float random = Random.Range(0, 4);
			float x = 0, y = 0;
			if (random < 1) {
				x = -0.2f;
				y = Random.Range(0, 1);
			} else if (random < 2) {
				x = 1.2f;
				y = Random.Range(0, 1);
			} else if (random < 3) {
				x = Random.Range(0, 1);
				y = -0.2f;
			} else {
				x = Random.Range(0, 1);
				y = 1.2f;
			}
			Vector3 startingPosition = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));

			Instantiate(
        		tempAsteroid,
        		startingPosition,
        		Quaternion.identity
        		);
		}

		GameObject.Find("Astronaut").transform.Rotate (new Vector3 (0, 0, Random.Range(5, 25)) * Time.deltaTime);
	}
}
