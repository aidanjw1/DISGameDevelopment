/// <summary>
/// Manages laser obstacle
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	[Tooltip("time offset for laser")]
	public int timeOffset;
	[Tooltip("Initializes shield prefab")]
	public int waitTime;
	[Tooltip("bool that checks if laser if on")]
	private bool laserOn;

	// Use this for initialization
	void Start () {
		laserOn = false;
		StartCoroutine (StartLaser ());
	}
		
	/// <summary>
	/// coroutine for laser offset
	/// </summary>
	/// <returns>The laser.</returns>
	IEnumerator StartLaser() {
		yield return new WaitForSeconds (timeOffset);
		StartCoroutine (LaserCoroutine ());
	}

	/// <summary>
	/// coroutine that enables laser
	/// </summary>
	/// <returns>The coroutine.</returns>
	IEnumerator LaserCoroutine() {
		while (true) {
			yield return new WaitForSeconds (waitTime);
			// set active/inactive
			gameObject.GetComponent<SpriteRenderer>().enabled = laserOn;
			gameObject.GetComponent<BoxCollider2D> ().enabled = laserOn;
			laserOn = !laserOn;
		}
	}

	/// <summary>
	/// Raises the collision enter2 d event. Harms player if player contacts laser
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Player") {
			GameObject playerdeath = GameObject.FindGameObjectWithTag ("playerdeath");
			playerdeath.GetComponent<AudioSource> ().Play ();
			other.gameObject.GetComponent<Player> ().Die ();
		}
	}
}
