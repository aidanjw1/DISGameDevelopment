using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions2 : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			Destroy (GameObject.FindWithTag("left click"));
		} 
		else if (Input.GetMouseButton(1)) {
			Destroy (GameObject.FindWithTag("right click"));
		} 
		
	}
}
