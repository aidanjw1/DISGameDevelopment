using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject FishSpawn;
	public GameObject hookPrefab;
	public float velocity;
	private bool casted;
	private float timeHeld = 0.0f;
	public LineRenderer lineRenderer;

	// Use this for initialization
	void Start () {
		casted = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!casted) {
			transform.position -= new Vector3 (0, velocity, 0);
		}

		if (transform.position.y <= -6 || transform.position.y >=-1.6) {
			velocity *= -1;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			casted = true;
			timeHeld = 0f;

		} 
		if (Input.GetKey (KeyCode.Space)) {
			timeHeld += Time.deltaTime;
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			Cast (timeHeld, transform.position);
			casted = false;
		}
	}

	void Cast(float timeHeld, Vector3 position) {
		float length = timeHeld * 5;
		lineRenderer.SetPosition (0, position);
		Vector3 end = new Vector3 (length, 0, 0);
		lineRenderer.SetPosition (1, position + end);	
		lineRenderer.enabled = true;
		hookPrefab.transform.position = position + end;
		Instantiate (hookPrefab);
		StartCoroutine (KillLine ());
	}

	IEnumerator KillLine() {
		yield return new WaitForSeconds (1);
		lineRenderer.enabled = false;
	}
}
