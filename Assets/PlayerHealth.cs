using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float health;
	public float currentHealth;

	public float fuel;

	private float sliderDelta = 1f;

	// public Image damageImage;
	public Slider healthSlider;
	public Slider fuelSlider;
	public bool damaged;
	// Use this for initialization
	void Awake () {
		health = 100;
		fuel = 200;
		currentHealth = health;
		damaged = false;
	}

	// Update is called once per frame

	void FixedUpdate () {
		// if (damaged) {
		// 	damageImage.color = new Color (1f, 0f, 0f, 0.1f);
		// } else {
		// 	damageImage.color = Color.Lerp (damageImage.color, Color.clear, 5f *Time.deltaTime);
		// }
		// damaged = false;

		// "Slide" the slider values to update
		if (healthSlider.value < currentHealth) {
			healthSlider.value += sliderDelta;
		} else if (healthSlider.value > currentHealth) {
			healthSlider.value -= sliderDelta;
		}

		if (Mathf.Abs(fuelSlider.value - fuel) > sliderDelta) {
			if (fuelSlider.value < fuel) {
				fuelSlider.value += sliderDelta;
			} else if (fuelSlider.value > fuel) {
				fuelSlider.value -= sliderDelta;
			}
		} else {
			fuelSlider.value = fuel;
		}
		
		fuel -= 0.05f;
	}

	public void increaseFuel() {
		fuel += 50;
		fuel = Mathf.Min(100, fuel);
	}

	public void useFuel() {
		fuel -= 0.15f;
	}

	public void getHit(){
		damaged = true;
		currentHealth -= 10;
	}

	public void increaseHealth(){
		currentHealth += 20;
		currentHealth = Mathf.Min(100, currentHealth);
	}
}