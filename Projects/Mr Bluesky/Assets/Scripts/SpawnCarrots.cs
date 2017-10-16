/// <summary>
/// Spawns carrots on win screen
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCarrots : MonoBehaviour {
	public GameObject balloon;
	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnBalloonCoroutine());
	}
	
	/// <summary>
	/// Spawns balloons
	/// </summary>
	/// <returns>The balloon coroutine.</returns>
	IEnumerator SpawnBalloonCoroutine () {
		while (true)
		{
			Instantiate(balloon, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
