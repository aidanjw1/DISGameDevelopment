using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Turret : MonoBehaviour {
	[Tooltip("Rate of fire of turrets")]
	public float rateOfFire = 1;
	[Tooltip("GameObject for projectiles")]
	public GameObject projectilePrefab;
	[Tooltip("float keeps track of last time laser was fired")]
	private float lastTimeFired = 0;
	[Tooltip("boolean for if player is within turret range")]
	public bool inRange = false;
	
	// Update is called once per frame
	void Update () {
		RangeCheck ();
		if ((lastTimeFired + 1 / rateOfFire) < Time.time && inRange == true) {
			lastTimeFired = Time.time;
			Fire ();
		}
	}

	/// <summary>
	/// Create instance of Projectile
	/// </summary>
	void Fire() {
		GameObject projectileObject1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
		Projectile projectile1 = projectileObject1.GetComponent<Projectile> ();
	}

	/// <summary>
	/// Checks to see if player is within certain range of turret. If so, returns true. Else, false.
	/// </summary>
	/// <returns><c>true</c>, if check was ranged, <c>false</c> otherwise.</returns>
	bool RangeCheck() {
		
		//get magnitude of the vector from player to turret
		Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform.position;
		Vector2 turretPosition = transform.position;
		Vector2 direction = turretPosition - playerPosition;
		float distance = direction.magnitude;

		//if magnitude is less than 30, player is in range
		if (distance < 30) {
			inRange = true;
		} else {
			inRange = false;
		}
		return inRange;
	}
}
