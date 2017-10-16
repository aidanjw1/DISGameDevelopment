using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyLevelScript : MonoBehaviour {

	public string levelName;
	// Use this for initialization

	// Update is called once per frame
	void OnCollisionEnter2D (Collision2D other){
		Text enemy = GameObject.FindGameObjectWithTag("enemyCount").GetComponent<Text> ();

		if (enemy.text.Equals ("0")) {
			SceneManager.LoadScene (levelName);
		}
	}
}
