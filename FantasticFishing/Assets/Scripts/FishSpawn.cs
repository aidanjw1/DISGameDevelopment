using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour {

	public GameObject fishPrefab1;
	public int probFish1;
	public GameObject fishPrefab2;
	public int probFish2;
	public GameObject fishPrefab3;
	public int probFish3;
	public GameObject fishPrefab4;
	public int probFish4;
	public GameObject fishPrefab5;
	public int probFish5;
	public GameObject fishPrefab6;
	public int probFish6;
	public GameObject applePrefab;
	public int appleProb;

	private List<GameObject> Fish;

	Dictionary <string, int> pointsDictionary;

	// Use this for initialization
	void Start () {
		Fish = new List<GameObject> ();
		AddFishToList (fishPrefab1, probFish1);
		AddFishToList (fishPrefab2, probFish2);
		AddFishToList (fishPrefab3, probFish3);
		AddFishToList (fishPrefab4, probFish4);
		AddFishToList (fishPrefab5, probFish5);
		AddFishToList (fishPrefab6, probFish6);
		AddFishToList (applePrefab, appleProb);

		pointsDictionary = new Dictionary<string, int> ();
		pointsDictionary.Add ("apple", -1);
		pointsDictionary.Add ("blob", 1);
		pointsDictionary.Add ("blue", 2);
		pointsDictionary.Add ("doublehead", 3);
		pointsDictionary.Add ("snake", 3);
		pointsDictionary.Add ("octopus", 10);
		pointsDictionary.Add ("red", 2);
	}

	private void AddFishToList(GameObject prefab, int probability) {
		for (int i = 0; i < probability; i++) {
			Fish.Add (prefab);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnFish (Vector3 position) {
		int random = Random.Range (0, Fish.Count);
		GameObject FishToSpawn = Fish [random];
		FishToSpawn.transform.position = position;
		Instantiate (FishToSpawn);
		GameObject Score = GameObject.Find ("Score");
		Score.GetComponent<ScoreScript> ().AddPoints(pointsDictionary[FishToSpawn.tag.ToString()]);
	}
}
