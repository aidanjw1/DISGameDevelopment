using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumble : MonoBehaviour {

	public float CrumbleTime;

	// Use this for initialization
	void Start () {
		StartCoroutine (CrumbleCoroutine ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator CrumbleCoroutine() {
		yield return new WaitForSeconds (CrumbleTime);
		Destroy (gameObject);
	}
}
