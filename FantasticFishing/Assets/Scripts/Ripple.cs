using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour {

	public float rippleTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (RippleCoroutine ());
	}
	
	// Update is called once per frame
	void Update () {

	}

	IEnumerator RippleCoroutine() {
		yield return new WaitForSeconds (rippleTime);
		Destroy (gameObject);
	}

	public void Death() {
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Hook") {
			GameObject FishSpawn = GameObject.Find ("FishSpawn");
			FishSpawn.GetComponent<FishSpawn> ().SpawnFish (transform.position);
			Destroy (gameObject);
		}
	}




}
