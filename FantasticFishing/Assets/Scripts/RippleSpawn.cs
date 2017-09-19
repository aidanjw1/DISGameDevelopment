using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleSpawn : MonoBehaviour {

	public GameObject ripplePrefab;
	public float waterEdge;
	public float minTime;
	public float maxTime;
	public GameObject FishSpawn;


	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnRippleCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
	}

	IEnumerator SpawnRippleCoroutine() {
		while (true) {
			yield return new WaitForSeconds (Random.Range(minTime, maxTime));
			float posX = Random.Range (0, 8);
			float posY = Random.Range (waterEdge, waterEdge - 2);
			ripplePrefab.transform.position = new Vector3 (posX, posY, -1);
			Instantiate (ripplePrefab);
		}
	}
}
