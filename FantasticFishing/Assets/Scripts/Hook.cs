using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (KillHook ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator KillHook() {
		yield return new WaitForSeconds (1);
		Destroy (gameObject);
	}
}
