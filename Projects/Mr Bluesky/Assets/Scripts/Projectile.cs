/// <summary>
/// Script controls projectile once it is instantiated. 
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
	[Tooltip("Projectile lifetime")]
	public float lifeTime = 4;

	// Use this for initialization
	void Start () {
		Vector2 newDirection = TargetPlayer ();
		GetComponent<Rigidbody2D>().velocity = newDirection.normalized * 10;
		StartCoroutine (KillAfterSeconds (lifeTime));
		IgnoreProjectiles ();
		IgnoreTurret ();
		StartCoroutine (ExecuteAfterTime(0.5f));
	}
		
	/// <summary>
	/// Manages collisions between projectile and other game objects
	/// </summary>
	/// <param name="other">Other.</param>
	void OnCollisionEnter2D(Collision2D other) {
		// Player loses and heart, projectile despawns, and player audio is triggered
		if (other.collider.CompareTag ("Player")) {
			GameObject.FindGameObjectWithTag ("playerdeath").GetComponent<AudioSource>().Play();
			Destroy (gameObject);
			other.gameObject.GetComponent<Player> ().Die ();
		} else if (other.collider.CompareTag ("shield")) {
			//Does nothing if it collides with shield
			
		} else if (other.collider.CompareTag ("turret")) {
			//turret is destroyed, turret death audio is triggered, projectile despawns
			GameObject.FindGameObjectWithTag ("enemydeath").GetComponent<AudioSource>().Play();
			EnemyCount.KilledEnemy ();
			Destroy (gameObject);
			Destroy (other.collider.gameObject);
		} else {
			// if projectile hits any other object it is destroyed
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Finds vector between player and projectile
	/// </summary>
	/// <returns>normalized vector between projectile and player</returns>
	Vector2 TargetPlayer() {
		Vector2 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().transform.position;
		Vector2 turretPosition = transform.position;
		Vector2 direction = playerPosition - turretPosition;
		direction = direction.normalized * 18;
		return direction;
	}
		
	/// <summary>
	/// Kills projectile after number of seconds
	/// </summary>
	/// <returns>The after seconds.</returns>
	/// <param name="seconds">Seconds.</param>
	IEnumerator KillAfterSeconds (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		Destroy (gameObject);
	}


	/// <summary>
	/// Ignores collisions between projectiles
	/// </summary>
	void IgnoreProjectiles() {
		GameObject[] projectiles;
		projectiles = GameObject.FindGameObjectsWithTag("projectile");
		foreach (GameObject projectile in projectiles) {
			Projectile projectile1 = projectile.GetComponent<Projectile> (); 
			Physics2D.IgnoreCollision(projectile1.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
		}
	}

	/// <summary>
	/// Initially ignores collision with turret
	/// </summary>
	void IgnoreTurret() {
		GameObject[] turrets;
		turrets = GameObject.FindGameObjectsWithTag ("turret");
		foreach (GameObject turret in turrets) {
			Turret turret1 = turret.GetComponent<Turret> (); 
			Physics2D.IgnoreCollision(turret1.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>());
		}

	}

	/// <summary>
	/// Executes the after time. Allows collions between projectiles and turrets after a certain amount of time has passed
	/// </summary>
	/// <returns>The after time.</returns>
	/// <param name="time">Time.</param>
	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);

		GameObject[] turrets;
		turrets = GameObject.FindGameObjectsWithTag ("turret");
		foreach (GameObject turret in turrets) {
			Turret turret1 = turret.GetComponent<Turret> (); 
			Physics2D.IgnoreCollision(turret1.GetComponent<CircleCollider2D>(), GetComponent<CircleCollider2D>(), false);
		}
	}
}
