using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int health;
	public int currentHealth;
	public Image damageImage;
	public Slider healthSlider;
	public bool damaged;
	// Use this for initialization
	void Awake () {
		health = 100;
		currentHealth = health;
		damaged = false;
	}

	// Update is called once per frame
	void Update () {
		if (damaged) {
			damageImage.color = new Color (1f, 0f, 0f, 0.1f);
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, 5f *Time.deltaTime);
		}
		damaged = false;

	}

	public void getHit(){
		damaged = true;
		currentHealth -= 10;
		healthSlider.value = currentHealth;

	}

	public void increaseHealth(){
		if (currentHealth <= 95) {
			currentHealth += 5;
			healthSlider.value = currentHealth;
		} else {
			currentHealth = 100;
			healthSlider.value = currentHealth;
		}
	}
}