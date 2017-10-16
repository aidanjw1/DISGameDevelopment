using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	public int pointsToAdd;
	public GameObject score;
	public AudioSource coinSound;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Player> () == null) {
			return;
		}
		coinSound.Play ();
		ScoreManager.AddPoints (pointsToAdd);
		ScoreManager.Subtract ();
		Destroy (gameObject);
	}
}
