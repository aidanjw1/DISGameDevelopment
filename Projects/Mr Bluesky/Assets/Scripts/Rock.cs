/// <summary>
/// Kills the spawned rock prefab after a set amount of time
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

	public static float rockTime;
	public GameObject crumblePrefab;

	// Use this for initialization
	void Start () {
		StartCoroutine (KillRockCoroutine ());
	}

	/// <summary>
	/// Kills the rock after rockTime.
	/// </summary>
	/// <returns>The rock coroutine.</returns>
	IEnumerator KillRockCoroutine() {
		yield return new WaitForSeconds (rockTime);
		GameObject.FindObjectOfType<Draw> ().loseRock ();
		crumblePrefab.transform.position = gameObject.transform.position;
		Instantiate (crumblePrefab);
		Destroy (gameObject);
	}
}
