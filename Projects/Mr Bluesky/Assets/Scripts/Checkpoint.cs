/// <summary>
/// Script manages checkpoint mechanic
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	[Tooltip("Initializes animator")]
	Animator myAnim;
	[Tooltip("boolean for when player reaches checkpoint")]
	bool reached = false;
	[Tooltip("vector between checkpoint and endpoint")]
	Vector3 direction;

	void Start () {
		myAnim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		//updates bird position only if check point had been reached
		if (reached == true) {
			transform.position += direction;	
		}
	}

	/// <summary>
	/// Raises the trigger enter2 d event. Animates bird when check point is reached. Destroys bird once it collides with end point.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Player") {
			//setting the state to 1 to begin checkpoint animation
			myAnim.SetInteger ("State", 1); 
			StartCoroutine (ExecuteAfterTime (2.4f));
		} else if (other.CompareTag("EndPoint")) {
			Destroy (gameObject);
		}
	}

	/// <summary>
	/// Acitivates bird check point and causes bird to fly towards end point and then destroy itself on collision
	/// </summary>
	void FlyAway() {
		reached = true;
		//Players respawn position is changed to checkpoint location
		Vector3 position = this.gameObject.transform.position;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().currentPlayerPosition = position;

		//find vector between checkpoint and endpoint
		Vector2 endpointPosition = GameObject.FindGameObjectWithTag ("EndPoint").transform.position;
		Vector2 checkpointPosition = transform.position;
		direction = (endpointPosition - checkpointPosition).normalized * .2f;
	}

	/// <summary>
	/// Executes the after 2 seconds. Gives time for animation to start before moving bird.
	/// </summary>
	/// <returns>The after time.</returns>
	/// <param name="time">Time.</param>
	IEnumerator ExecuteAfterTime(float time)
	{
		yield return new WaitForSeconds(time);
		FlyAway ();
	}
}