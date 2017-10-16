/// <summary>
/// Manages player collisions with bottom of the map
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {

	/// <summary>
	/// Raises the collision enter2 d event.
	/// </summary>
	/// <param name="coll">Coll.</param>
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("hit the water");
			Debug.Log ("calling die");
			coll.gameObject.GetComponent<Player> ().Die (true);
		
		}
	}
}
