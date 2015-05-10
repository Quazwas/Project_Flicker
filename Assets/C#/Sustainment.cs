using UnityEngine;
using System.Collections;

public class Sustainment : MonoBehaviour {

	public float hunger = 50;
	public float maxHunger = 100;
	public float thirst = 50;
	public float maxThirst= 100;
	public float infection = 50;
	public float maxInfection= 100;
	public float stamina = 50;
	public float maxStamina= 100;
	public float health = 50;
	public float maxHealth= 100;

	public float hungerRate = -0.01f;
	public float thirstRate = -0.01f;

	void Update() {
		modHunger (hungerRate*Time.deltaTime);
		modThirst (thirstRate*Time.deltaTime);
	}

	public void modHunger(float mod) {
		hunger += mod;
		if (hunger > maxHunger) {
			hunger = maxHunger;
		} else if (hunger <= 0) {
			hunger = 0;
		}
	}
	public void modThirst(float mod) {
		thirst += mod;
		if (thirst > maxThirst) {
			thirst = maxHunger;
		} else if (hunger <= 0) {
			thirst = 0;
		}
	}
	public void modInfecton(float mod) {
		infection += mod;
		if (infection > maxInfection) {
			infection = maxInfection;
		} else if (infection <= 0) {
			infection = 0;
		}
	}
	public void modStamina(float mod) {
		stamina += mod;
		if (stamina > maxStamina) {
			stamina = maxStamina;
		} else if (stamina <= 0) {
			stamina = 0;
		}
	}
	public void modHealth(float mod) {
		health += mod;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health <= 0) {
			health = 0;
			kill ();
		}
	}

	public void kill() {
		print ("You Dead");
	}


	void OnGUI() {
		GUI.Label (new Rect(600, 100, 150,30), "Health: " + health.ToString());
		GUI.Label (new Rect(600, 120, 150,30), "Hunger: " + hunger.ToString());
		GUI.Label (new Rect(600, 140, 150,30), "Thirst: " +thirst.ToString());
		GUI.Label (new Rect(600, 160, 150,30), "Infection: " +infection.ToString());
		GUI.Label (new Rect(600, 180, 150,30), "Stamina: " +stamina.ToString());
	}

}
