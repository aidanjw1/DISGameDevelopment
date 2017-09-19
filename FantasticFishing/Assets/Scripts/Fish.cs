using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (KillFish ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator KillFish() {
		yield return new WaitForSeconds (3);
		Destroy (gameObject);
	}
}
